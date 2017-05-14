using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : ScriptableObject {
    public string name;
    public ArrayList roster;
    public int points;
    public List<Team> scheduleOfOpponents;
    public Team currentOpponentTeam;
    public int matchesPlayed;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize()
    {
    }
}
