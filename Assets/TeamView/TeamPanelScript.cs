using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamPanelScript : MonoBehaviour {
    public GameObject characterPlatePrefab;
    public HomeScreenScript homeScreenScript;

    
    // Use this for initialization
    void Start () {
        //print("calling format roster");
        //formatPlayerRosterForTeamPanel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void formatPlayerRosterForTeamPanel()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < HomeScreenScript.teamList[0].roster.Count; i++)
        {
            Character pc = HomeScreenScript.teamList[0].roster[i] as Character;
            GameObject characterPlate = Instantiate(characterPlatePrefab) as GameObject;
            characterPlate.GetComponent<CharacterPlateScript>().Initialize(pc, characterPlate.GetComponent<Button>(), false);
            characterPlate.transform.SetParent(gameObject.transform, false);
            //print("formatting " + i);
        }
    }

    public void formatScoutableFightersForTeamPanel()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < HomeScreenScript.scoutableFighters.Count; i++)
        {
            Character pc = HomeScreenScript.scoutableFighters[i] as Character;
            GameObject characterPlate = Instantiate(characterPlatePrefab) as GameObject;
            characterPlate.GetComponent<CharacterPlateScript>().Initialize(pc, characterPlate.GetComponent<Button>(), false, true);
            characterPlate.transform.SetParent(gameObject.transform, false);
            //print("formatting " + i);
        }
    }
}
