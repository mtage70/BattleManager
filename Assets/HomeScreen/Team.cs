using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Team : ScriptableObject {
    public string name;
    public ArrayList roster;
    public int points;
    public List<Team> scheduleOfOpponents;
    public Team currentOpponentTeam;
    public int matchesPlayed;

    public int funds;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize()
    {
    }

    public Team CreateSpecificTeam(TeamData td) {
        Team temp = new Team();
        temp.name = td.name;
        temp.points = td.points;
        temp.roster = new ArrayList();
        foreach(CharacterData cd in td.cldata) {
            temp.roster.Add(Character.CreateSpecificCharacter(cd));
        }
        
        temp.matchesPlayed = td.matchesPlayed;
        temp.funds = td.funds;
        return temp;
    }
}
