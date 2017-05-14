using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLineupButtonScript : MonoBehaviour {
    Button lineupButton;
    public GameObject strategyPanel;
    public GameObject lineupPanel;
	// Use this for initialization
	void Start () {
        lineupButton = gameObject.GetComponent<Button>();
        lineupButton.onClick.AddListener(OpenLineupPanel);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenLineupPanel()
    {
        lineupPanel.SetActive(true);
        strategyPanel.SetActive(false);
        lineupPanel.GetComponentInChildren<LineupPanelScript>().formatPlayerRosterForLineupPanel();
        
    }
}
