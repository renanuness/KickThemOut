using UnityEngine;
using System.Collections;
using System;

public class HearingBehaviour : MonoBehaviour {

    //---------------Shoot--------
    public LineRenderer laserLine;

    //----------------------------


    public FieldOfView fv;
    public GameObject player;
    public GameObject HordeController;
    // Use this for initialization

    void Awake() {
        laserLine = GetComponent<LineRenderer>();
        fv = GetComponent<FieldOfView>();
        HordeController = GameObject.FindGameObjectWithTag("HordeController");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (fv.GetPursuit()) {
            ShootInPlayer();
        }
        Vector3  targetPoint= HordeController.GetComponent<HearingController>().GetPosition();
        if(targetPoint != null && (targetPoint.x != transform.position.x && targetPoint.y != transform.position.y)) {
            ShootPosition(targetPoint);
        }else {
            Stop();
        }
    }

    public void ShootPosition(Vector3 targetPoint) {
        if (Vector3.Distance(targetPoint, player.transform.position) < 15) {
            transform.LookAt(targetPoint);
            Debug.Log(targetPoint);
            gameObject.GetComponent<Animator>().SetBool("walk", true);
            transform.position += transform.forward * Time.deltaTime * 15f;
        }

    }

    void ShootInPlayer() {
        laserLine.enabled = true;
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y+2,transform.position.z), Vector3.forward, out hit, 200)) {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Player")) {
                try {
                    Debug.Log("Corre berg");
                } catch (Exception ie) {
                    print(ie);
                }
            }
        }
    }

    void Stop() {
        Debug.Log("Parar");
        gameObject.GetComponent<Animator>().SetBool("walk", false);
        transform.position += transform.forward * Time.deltaTime * 0f;

    }
}
