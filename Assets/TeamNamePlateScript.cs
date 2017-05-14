using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamNamePlateScript : MonoBehaviour {
    public string name;
	// Use this for initialization
	void Start () {
        GetComponentInChildren<Text>().text = name;
        GetComponentInChildren<Button>().onClick.AddListener(TeamNamePlateButtonOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(string s)
    {
        name = s;
    }

    public void TeamNamePlateButtonOnClick()
    {
        HomeScreenScript.playerTeamName = name;
        SceneManager.LoadScene("HomeScreen");
    }
}
