using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;
public class GameController : MonoBehaviour {

    public Transform pauseMenu;
    public Transform player;
    public Transform exitMenu;

    public int level;
    public Weapon equippedGun;
    public List<int> ammoTotal;
    public List<int> ammoMagazine;
    public int playerHealth;

    public HordeDatabase Enemy;
    public GameObject Roof;
    public GameObject Block;
    public GameObject StandPoint;
    public Transform[] roofs;
    public Transform[] blocks;
    public Transform[] standPoints;
	// Use this for initialization
	void Start () {
        SetBlock();
        SetRoof();
        SetStandPoint();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }
        
    }

    void SetRoof() {
        roofs = new Transform[Roof.transform.childCount];
        Debug.Log("Size:"+roofs.Length);
        for (int i = 0; i < Roof.transform.childCount; i++) {
            roofs[i] = Roof.transform.GetChild(i);
        }
        for (int i = 0; i < Enemy.horde[0].snipers; i++) {
            Instantiate(Enemy.horde[0].sniper, roofs[i].position, roofs[i].rotation);
        }
    }

    void SetBlock() {
        blocks = new Transform[Block.transform.childCount];
        for (int i = 0; i < Block.transform.childCount; i++) {
            blocks[i] = Block.transform.GetChild(i);
        }
        for (int i = 0; i < Enemy.horde[0].patrols; i++) {
            Instantiate(Enemy.horde[0].patrol, blocks[i].position, blocks[i].rotation);
            Enemy.horde[0].patrol.GetComponent<PatrolBehaviour>().SetBlock(blocks[i].gameObject);
        }
    }

    void SetStandPoint() {
        standPoints = new Transform[StandPoint.transform.childCount];
        for (int i = 0; i < StandPoint.transform.childCount; i++) {
            standPoints[i] = StandPoint.transform.GetChild(i);
        }
        for (int i = 0; i < Enemy.horde[0].runners; i++) {
            Instantiate(Enemy.horde[0].runner, standPoints[i].position, standPoints[i].rotation);
        }
    }

    public void Pause() {
        if (pauseMenu.gameObject.activeInHierarchy == false) {
            UnPause();
        } else {
            Resume();            
        }
    }

    public void UnPause() {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        player.GetComponent<PlayerBehaviour>().enabled = false;
        player.GetComponentInChildren<CamMouseLook>().enabled = false;
    }
    public void Resume() {
        Debug.Log("dasdas");
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        player.GetComponent<PlayerBehaviour>().enabled = true;
        player.GetComponentInChildren<CamMouseLook>().enabled = true;
        
    }

    public void Exit() {
        exitMenu.gameObject.SetActive(true);
    }

    public void YesExit() {
        SceneManager.LoadScene(0);
    }

    public void NoExit() {
        exitMenu.gameObject.SetActive(false);
    }
    

}
