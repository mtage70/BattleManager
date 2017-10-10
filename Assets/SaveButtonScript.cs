using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveButtonScript : MonoBehaviour {
	public Button saveButton;
	// Use this for initialization
	void Start () {
        saveButton.onClick.AddListener(SaveButtonOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SaveButtonOnClick() {
		SaveLoadManager.SaveTeamList(HomeScreenScript.teamList);
		SaveLoadManager.SaveSchedule1();
		SaveLoadManager.SaveSchedule2();
	}
}
