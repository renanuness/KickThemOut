using UnityEngine;
using System.Collections;

public class FlashBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        StartCoroutine(TurnOff());

    }

    IEnumerator TurnOff() {
        yield return new WaitForSeconds(0.07f);
        gameObject.SetActive(false);        
    }
}
