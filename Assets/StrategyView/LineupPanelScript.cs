using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineupPanelScript : MonoBehaviour {
    public GameObject characterPlatePrefab;
    public HomeScreenScript homeScreenScript;
    public static int chosenCount;
    public static ArrayList chosenRoster = new ArrayList(){};
    public Button readyButton;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (chosenCount == 5)
        {
            readyButton.GetComponent<Image>().color = UnityEngine.Color.green;
        }
        else
        {
            readyButton.GetComponent<Image>().color = UnityEngine.Color.white;

        }
    }

    public void formatPlayerRosterForLineupPanel()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < ((Team)HomeScreenScript.teamList[0]).roster.Count; i++)
        {
            Character pc = ((Team)HomeScreenScript.teamList[0]).roster[i] as Character;
            GameObject characterPlate = Instantiate(characterPlatePrefab) as GameObject;
            characterPlate.GetComponent<CharacterPlateScript>().Initialize(pc, characterPlate.GetComponent<Button>(), true);
            characterPlate.transform.SetParent(gameObject.transform, false);
        }

    }
}
