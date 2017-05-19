using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Horde{

    public int number;

    public GameObject sniper;
    public GameObject patrol;
    public GameObject runner;

    public int snipers;
    public int patrols;
    public int runners;

    public List<GameObject> sniperPosition;
    public List<GameObject> patrolsPosition;
    public List<GameObject> runnerPosition;

    //public void SetPosition() {
    //    for(int i = 0; i < sniperPosition.Count; i++) {

    //    }

    //}


}
