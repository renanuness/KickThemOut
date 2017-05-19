using UnityEngine;
using System.Collections;

public class PatrolBehaviour : MonoBehaviour {

    //public Transform point1;
    //public Transform point2;
    //public Transform point3;
    //public Transform point4;

    public Transform initialPoint;
    public Transform targetPoint;

    public GameObject block;

    public int currentIndex;
    public bool going;
    // Use this for initialization
    void Awake() {
        int vectorSize = block.transform.childCount;
        initialPoint = block.transform.GetChild(0);
        transform.position = new Vector3(initialPoint.transform.position.x+8,this.transform.position.y, initialPoint.position.z);
        currentIndex = 0;
        going = true;
        targetPoint = block.transform.GetChild(currentIndex + 1);
    }
    void Start () {
            //point1.position.x, point1.position.y, point1.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        Ronda(targetPoint);


    }

    public void Ronda(Transform targetPoint) {
        transform.LookAt(targetPoint);
        gameObject.GetComponent<Animator>().SetBool("walk", true);
        transform.position += transform.forward * Time.deltaTime * 25f;
        
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "PatrolPoint") {
            StartCoroutine(DesableObs(other));
            NextPoint();

        }

    }

    void NextPoint() {

        if (currentIndex == 3) {
            going = false;
            Debug.Log("Último");
        }
        if(currentIndex == 0) {
            going = true;
        }
         
        if(going == false) {
            targetPoint = block.transform.GetChild(currentIndex - 1);
            currentIndex--;
        }else {
            targetPoint = block.transform.GetChild(currentIndex + 1);
            currentIndex++;
        }
        //Ronda(targetPoint);
        Debug.Log("Chegou");

    }

    IEnumerator DesableObs(Collider obs) {
        obs.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        obs.gameObject.SetActive(true);
    }

    public void SetBlock(GameObject blck) {
        block = blck;
    }
}
