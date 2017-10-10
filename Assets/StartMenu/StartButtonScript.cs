using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class StartButtonScript : MonoBehaviour {

    public Button startButton;
    public GameObject teamSelectionPanel;
	// Use this for initialization
	void Start () {
        Button sButton = startButton.GetComponent<Button>();
        sButton.onClick.AddListener(StartButtonClicked);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartButtonClicked ()
    {
        if (File.Exists(Application.persistentDataPath + "/teamList.sav")){
            HomeScreenScript.teamList = SaveLoadManager.LoadTeamList();
            foreach(Team t in HomeScreenScript.teamList) {
                print(t.name);
            }
            SaveLoadManager.LoadSchedule1();
            SaveLoadManager.LoadSchedule2();
            SceneManager.LoadScene("HomeScreen");
        }
        else {
            teamSelectionPanel.SetActive(true);
        }
            
        
    }
}
