using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaguePanelScript : MonoBehaviour {
    public GameObject teamPlatePrefab;
    public ArrayList pointSortedTeamList = new ArrayList();
    public string playerPosition;
	// Use this for initialization
	void Start () {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        quickUpdate();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void quickUpdate()
    {
        pointSortedTeamList.Clear();
        foreach (Team t in HomeScreenScript.scheduleList1)
        {
            pointSortedTeamList.Add(t);
        }
        foreach (Team t in HomeScreenScript.scheduleList2)
        {
            pointSortedTeamList.Add(t);
        }
        LeagueTablePointsComparer pointsComparer = new LeagueTablePointsComparer();
        pointSortedTeamList.Sort(pointsComparer);

        for (int i = 0; i < pointSortedTeamList.Count; i++)
        {
            Team teamEntry = pointSortedTeamList[i] as Team;
            GameObject teamPlate = Instantiate(teamPlatePrefab) as GameObject;
            teamPlate.GetComponent<TeamPlateScript>().Initialize(teamEntry, i+1);
            teamPlate.transform.SetParent(gameObject.transform, false);
            if (teamEntry.name == HomeScreenScript.playerTeamName)
            {
                playerPosition = ReferenceMaterial.AddOrdinal(i + 1);
                teamPlate.GetComponentInChildren<Image>().color = UnityEngine.Color.green;
            }
        }
        if (HomeScreenScript.matchesPlayed == 60)
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().color = Color.yellow;
        }
    }

}
