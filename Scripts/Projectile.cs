using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float bulletSpeed;
    public string gunName;
    public WeaponController gun;
    public Transform muzzler;
    // Use this for initialization


    void Awake() {
        gun = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponController>();
        gunName = gun.equippedGun.name;
        bulletSpeed = gun.equippedGun.fireVelocity;

    }

    public void Shoot (Weapon equippedGun) {
        muzzler = equippedGun.muzzle;
        Instantiate(equippedGun.bullet, muzzler.position, muzzler.rotation);
        Debug.Log("Velocidade: "+equippedGun.fireVelocity);
        //SetSpeed(equippedGun.fireVelocity);
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("What a head shot man!");
        
    }
    // Update is called once per frame
    void Update () {
        transform.Translate(Vector3.up* Time.deltaTime * bulletSpeed);
        //Destroy(gameObject, 5);
    }

    public void SetSpeed(float fireVelocity) {
        bulletSpeed = fireVelocity;

    }


}
