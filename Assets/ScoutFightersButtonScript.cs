using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoutFightersButtonScript : MonoBehaviour {

    public Button scoutButton;
	public Button teamButton;
    public GameObject teamPanel;
    public GameObject homePanel;
    public GameObject lineupPanel;
    public bool active = false;
    private Color activatedColor = UnityEngine.Color.green;
    private Color deactivatedColor = UnityEngine.Color.white;
    // Use this for initialization
    void Start()
    {
        Button tButton = scoutButton.GetComponent<Button>();
        tButton.onClick.AddListener(scoutButtonOnClick);
        scoutButton.GetComponent<Image>().color = deactivatedColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void scoutButtonOnClick()
    {
        if (active)
        {
            active = false;
            teamButton.GetComponent<Image>().color = deactivatedColor;
            teamPanel.SetActive(false);
            homePanel.SetActive(true);
            teamPanel.GetComponentInChildren<ScrollRect>().enabled = false;
        }
        else
        {
            active = true;
            teamButton.GetComponent<Image>().color = activatedColor;
            teamPanel.SetActive(true);
            homePanel.SetActive(false);
            lineupPanel.SetActive(false);
            LineupPanelScript.chosenCount = 0;
            LineupPanelScript.chosenRoster.Clear();
            teamPanel.GetComponentInChildren<ScrollRect>().enabled = true;
            teamPanel.GetComponentInChildren<TeamPanelScript>().formatScoutableFightersForTeamPanel();
			teamButton.GetComponent<TeamButtonScript>().active = true;

        }
        
    }
}

