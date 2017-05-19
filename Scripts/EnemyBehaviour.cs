using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public float health = 100;
    public int id;

    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject head;
    public GameObject chest;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(health<= 0) {
            gameObject.SetActive(false);
        }
	}

    public void Shooted(RaycastHit hitPoint, Weapon gun) {


        if(hitPoint.collider.gameObject.GetInstanceID() == leftArm.GetInstanceID()) {
            Debug.Log("Braço esquerdo");
            health -= (gun.damageFactor);

        } else if (hitPoint.collider.gameObject.GetInstanceID() == rightArm.GetInstanceID()) {
            Debug.Log("Braço direito");
            health -= (gun.damageFactor);

        } else if (hitPoint.collider.gameObject.GetInstanceID() == head.GetInstanceID()) {
            Debug.Log("Cabeça");
            health -= (gun.damageFactor * 1.5f);

        } else if(hitPoint.collider.gameObject.GetInstanceID() == chest.GetInstanceID()) {
            Debug.Log("Peito");
            health -= (gun.damageFactor * 1.25f);

        } else if(hitPoint.collider.gameObject.GetInstanceID() == leftLeg.GetInstanceID()) {
            Debug.Log("Perna esquerda");
            health -= (gun.damageFactor);

        } else if (hitPoint.collider.gameObject.GetInstanceID() == rightLeg.GetInstanceID()) {
            Debug.Log("Perna direita");
            health -= (gun.damageFactor);

        }

    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("OKfsdjfm");
    }
}
