using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelectScript : MonoBehaviour {
    public GameObject teamNamePlatePrefab;
	// Use this for initialization
	void Start () {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        

        for (int i = 0; i < HomeScreenScript.teamNames.Count; i++)
        {
            string teamEntry = HomeScreenScript.teamNames[i] as string;
            GameObject teamPlate = Instantiate(teamNamePlatePrefab) as GameObject;
            teamPlate.GetComponent<TeamNamePlateScript>().Initialize(teamEntry);
            teamPlate.transform.SetParent(gameObject.transform, false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
