using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        teamSelectionPanel.SetActive(true);
        
    }
}
