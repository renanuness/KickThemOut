using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class WeaponController : MonoBehaviour {

    public float nextShotTime;
    public float scoppedFOV = 15f;
    private float normalFOV;

    public LineRenderer laserLine;

    public Transform weaponHold;

    public WeaponDatabase database;

    public Weapon startingGun;
    public Weapon equippedGun;

    Color color = Color.red;


    public AudioSource audio;

    public GameObject aimPoint;
    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public GameObject gunImage;
    public GameObject ammoInformation;
    public GameObject hordeController;
    public Camera mainCamera;
    public bool currentState;

    
    
    //public List<Weapon> weapons;
    // Use this for initialization
    private void Awake() {
        database = GameObject.FindGameObjectWithTag("Weapon Database").GetComponent<WeaponDatabase>();
        startingGun = database.weapons[0];
        equippedGun = null;
        
    }
    void Start () {
        if (startingGun != null) {
            EquipGun(startingGun);
            currentState = equippedGun.gunModel.GetComponent<Animator>().GetBool("Aiming");
            
        }
    }
	
    public void EquipGun(Weapon guntToEquip) {
        if(equippedGun != null) {
            equippedGun.gunModel.SetActive(false);
        }
        guntToEquip.gunModel.SetActive(true);
        equippedGun = guntToEquip;
        SetGunInformation();
        ammoInformation.GetComponent<Text>().text = equippedGun.ammoMagazine +" | "+equippedGun.ammoTotal;
        laserLine = equippedGun.gunModel.GetComponent<LineRenderer>();
        audio = equippedGun.gunModel.GetComponent<AudioSource>();
    }

    public void AddAmmo(int gun, int ammo) {
        database.weapons[gun].ammoTotal += ammo;
    }

    public void Shoot() {
        if (equippedGun != null) {
            if (Time.time > nextShotTime) {
                if (equippedGun.ammoMagazine > 0) {
                    nextShotTime = Time.time + equippedGun.fireRating / 1000;
                    audio.clip = equippedGun.shootAudio;
                    laserLine.enabled = true;
                    RaycastHit hit;
                    if (Physics.Raycast(equippedGun.muzzle.transform.position, equippedGun.muzzle.TransformDirection(Vector3.forward),out hit ,equippedGun.range)){
                        Debug.Log(hit.collider.gameObject.name);
                        if (hit.collider.gameObject.CompareTag("Enemy")){
                            try {
                                hit.collider.gameObject.GetComponentInParent<EnemyBehaviour>().Shooted(hit, equippedGun);
                            } catch (Exception ie) {
                                print(ie);
                            }
                        }
                    }
                    hordeController.GetComponent<HearingController>().SetPosition(transform.position);
                    //HearingBehaviour hb = GetComponent<HearingBehaviour>();
                    equippedGun.flash.GetComponent<EllipsoidParticleEmitter>().Emit();
                    audio.Play();
                    equippedGun.ammoMagazine--;
                    SetGunInformation();

                } else {
                    Debug.Log("Out Ammo");
                }
            }
        } 
    }
    
    public void Recharge() {
        int bulletsToAdd;
        int magazineSpace = equippedGun.magazineSize - equippedGun.ammoMagazine;
        if(magazineSpace < equippedGun.ammoTotal) {
            bulletsToAdd = magazineSpace;
            equippedGun.ammoMagazine += bulletsToAdd;
            equippedGun.ammoTotal -= bulletsToAdd;
        }else {
            bulletsToAdd = equippedGun.ammoTotal;
            equippedGun.ammoMagazine += bulletsToAdd;
            equippedGun.ammoTotal -= bulletsToAdd;
        }
        SetGunInformation();
    }

    public void ChangeGun(int id) {
        if (equippedGun.id != id) {
            id = id - 1;
            Weapon gun = database.weapons[id];
            if (equippedGun.name == "SniperRifle" && currentState == true) {
                OnUnscoped();
            }
            EquipGun(gun);
            currentState = true;
            Aim();
        }
    }

    public void SetGunInformation() {
        ammoInformation.GetComponent<Text>().text = equippedGun.ammoMagazine + " | " + equippedGun.ammoTotal;
        gunImage.GetComponent<Image>().sprite = equippedGun.gunIcon;
    }
    public void Aim() {
        //equippedGun.gunModel.GetComponent<Animator>().Play("AimGun",0);
        if(currentState == false) {
            equippedGun.gunModel.GetComponent<Animator>().SetBool("Aiming", true);
            currentState = true;
            aimPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(7, 7);
        }else {
            equippedGun.gunModel.GetComponent<Animator>().SetBool("Aiming", false);
            currentState = false;
            //r = Physics.Raycast(equippedGun.muzzle.transform.position, equippedGun.muzzle.TransformDirection(Vector3.forward), equippedGun.range);
            aimPoint.GetComponent<RectTransform>().position = new Vector3(14f,4f);
            aimPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(15, 15);

        }
    }

    public void AimRifle() {

        if (currentState == false) {
            currentState = true;
            equippedGun.gunModel.GetComponent<Animator>().SetBool("Aiming", true);
            StartCoroutine(OnScoped());
        } else {
            OnUnscoped();
            equippedGun.gunModel.GetComponent<Animator>().SetBool("Aiming", false);
            currentState = false;
        }
    }

    IEnumerator OnScoped() {
        yield return new WaitForSeconds(.8f);
        weaponCamera.SetActive(false);
        scopeOverlay.SetActive(true);
        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scoppedFOV;
        aimPoint.SetActive(false);
    }

    void OnUnscoped() {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        mainCamera.fieldOfView = normalFOV;
        aimPoint.SetActive(true);
    }

    public void CalibrarMira() {
        Ray raio = new Ray(equippedGun.muzzle.transform.position, equippedGun.muzzle.transform.forward);
        Vector3 MiraPonto = Camera.main.WorldToScreenPoint(raio.GetPoint(equippedGun.range));
        if (equippedGun.id == 3 && currentState == true) {

            scopeOverlay.transform.position = new Vector2(MiraPonto.x, MiraPonto.y);
        }else {
            aimPoint.transform.position = new Vector2(MiraPonto.x, MiraPonto.y);

        }

    }

    public void GUIMunicao() {

    }
    // Update is called once per frame
    void Update() {
        CalibrarMira();
        Debug.DrawRay(equippedGun.muzzle.transform.position, equippedGun.muzzle.transform.forward*equippedGun.range, color,5,true);

    }
}
