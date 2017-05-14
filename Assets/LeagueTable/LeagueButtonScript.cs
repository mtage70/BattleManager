using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeagueButtonScript : MonoBehaviour {
    public GameObject leaguePanel;
    public GameObject homePanel;
    public GameObject lineupPanel;
    public GameObject teamPanel;
    private bool active = false;
    private Color activatedColor = UnityEngine.Color.green;
    private Color deactivatedColor = UnityEngine.Color.white;
    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(LeagueButtonOnClick);
        gameObject.GetComponent<Image>().color = deactivatedColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LeagueButtonOnClick ()
    {
        if (active)
        {
            active = false;
            gameObject.GetComponent<Image>().color = deactivatedColor;
            leaguePanel.SetActive(false);
            homePanel.SetActive(true);
            leaguePanel.GetComponentInChildren<ScrollRect>().enabled = false;
        }
        else
        {
            active = true;
            gameObject.GetComponent<Image>().color = activatedColor;
            leaguePanel.SetActive(true);
            homePanel.SetActive(false);
            lineupPanel.SetActive(false);
            teamPanel.SetActive(false);
            LineupPanelScript.chosenCount = 0;
            LineupPanelScript.chosenRoster.Clear();
            leaguePanel.GetComponentInChildren<ScrollRect>().enabled = true;


        }
    }
    
}
