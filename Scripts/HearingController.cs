using UnityEngine;
using System.Collections;

public class HearingController : MonoBehaviour {

    Vector3 shootPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    
    public void SetPosition(Vector3 position) {
        shootPosition = position;
    }

    public Vector3 GetPosition() {
        
        return shootPosition;
    }
}
