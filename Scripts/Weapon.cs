using UnityEngine;
using System.Collections;

[System.Serializable]
public class Weapon{

    
    public int id;
    public string name;
    public float fireRating;
    public float fireVelocity;
    public float nextShotTime;
    public int damageFactor;
    public int range;
    public int ammoTotal;
    public int ammoMagazine;
    public int magazineSize;
    public Transform muzzle;
    public GameObject gunModel;
    public GameObject bullet;
    public GameObject flash;
    public AudioClip shootAudio;
    public AudioClip reloadAudio;
    public Sprite gunIcon;
    //public GameObject aimImage;
    //public Animator animatorController;
    //public Weapon(int id, float fireRating, int damageFactor, int range, string name, int ammoTotal, int ammoMagazine, int magazineSize, Transform muzzle, GameObject gunModel,
    //    Component  shotSound, GameObject bullet) {
    //    this.id = id;
    //    this.fireRating = fireRating;
    //    this.damageFactor = damageFactor;
    //    this.range = range;
    //    this.name = name;
    //    this.ammoTotal = ammoTotal;
    //    this.ammoMagazine = ammoMagazine;
    //    this.magazineSize = magazineSize;
    //    this.muzzle = muzzle;
    //    this.gunModel = gunModel;
    //    this.bullet = bullet;
    //    this.shotSound = shotSound;
    //}
    
   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
