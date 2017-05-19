using UnityEngine;
using System.Collections;

public class Optimization : MonoBehaviour {

    public GameObject player;
    public GameObject[] builds;
    public float PlayerDistance;
    public float distanceMax = 40;

	// Use this for initialization
	void Start () {
        builds = GameObject.FindGameObjectsWithTag("Build");
        Debug.Log(builds);
    }
	
	// Update is called once per frame
	void Update () {
	    for(int i = 0; i < builds.Length; i++) {
            PlayerDistance = Vector3.Distance(player.transform.position, builds[i].transform.position);
            if (PlayerDistance > distanceMax) {
                builds[i].SetActive(false);
            }else {
                builds[i].SetActive(true);
            }
        }
	}
}
