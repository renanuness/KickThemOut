using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class PlayerBehaviour : MonoBehaviour {

	public float health;
    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
	public GameObject activeGun;
    public Image healthBar;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
        GunManagement();
        //GetDamage();
        healthBar.fillAmount = health / 100;

    }

    void Movement(){
		if (Input.GetAxis ("Run")>0) {
			speed = runSpeed;
		} else {
			speed = walkSpeed;
		}
        if (Input.GetKey(KeyCode.W)) {
            transform.position += transform.forward * Time.deltaTime * speed;
        } else if (Input.GetKey(KeyCode.S)) {
            transform.position += -transform.forward * Time.deltaTime * speed;
        } else if (Input.GetKey(KeyCode.A)) {
            transform.position += -transform.right * Time.deltaTime * speed;
        } else if (Input.GetKey(KeyCode.D)) {
            transform.position += transform.right * Time.deltaTime * speed;
        }
        //float vertical = Input.GetAxis ("Vertical");
        //float horizontal = Input.GetAxis ("Horizontal");
        //      float posX = transform.position.x;
        //      float posZ = transform.position.z;
        //      var c = transform.forward;
        if (Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
            transform.position = new Vector3(transform.position.x, transform.position.y * jumpForce , transform.position.z);

        }
        //      transform.position = new Vector3(c+horizontal*speed,this.transform.position.y,posZ+vertical*speed);
        //if (Input.GetAxis("Jump") > 0) {
        //    float jump = Input.GetAxis("Jump");
        //    float posY = transform.position.y;
        //    GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        //    transform.position = new Vector3(posX, posY * jumpForce * jump, posZ);
        //}
    }

    public void GunManagement() {
        WeaponController gun = GetComponent<WeaponController>();

        if (Input.GetMouseButtonDown(0)) {
            gun.Shoot();
        }

        if (Input.GetAxis("Pistol")>0) {
            gun.ChangeGun(1);
        }
        if (Input.GetAxis("AssaultRifle") > 0) {
            gun.ChangeGun(2);
        }
        if (Input.GetAxis("SniperRifle") > 0) {
            gun.ChangeGun(3);
        }
        if (Input.GetAxis("Recharge") > 0) {
            gun.Recharge();
        }
        if (Input.GetMouseButtonDown(1)) {
            if (gun.equippedGun.name == "SniperRifle") {
                gun.AimRifle();
            } else {
                gun.Aim();
            }
        }
    }
}
