using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineupBackButton : MonoBehaviour {
    public GameObject lineupPanel;
    public GameObject homePanel;
    Button btn;
	// Use this for initialization
	void Start () {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(LineUpBackButtonOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LineUpBackButtonOnClick()
    {
        lineupPanel.SetActive(false);
        homePanel.SetActive(true);
        LineupPanelScript.chosenCount = 0;
        LineupPanelScript.chosenRoster.Clear();
    }
}
