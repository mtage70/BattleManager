using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Collections;
using System.Runtime.CompilerServices;

public class HomeScreenScript : MonoBehaviour {
    public static bool firstTimeStartup = true;
    public static List<Team> teamList = new List<Team>() { };
    public static ArrayList deadCharacters = new ArrayList();
    public Transform teamPanel;
    public Transform lineupPanel;
    public Transform leaguePanel;
    public Transform newsfeedPanel;

    public static GameObject quickFinances;
    public static Team currentOpponentTeam;
    public static int currentOpponentTeamIndex = 0;
    public static List<Team> scheduleOfOpponents = new List<Team>() { };
    public static List<Team> scheduleList1 = new List<Team>() { };
    public static List<Team> scheduleList2 = new List<Team>() { };
    public static int matchesPlayed = 0;
    public static string playerTeamName = "";
    public static ArrayList teamNames = new ArrayList() { "The Baelfos Brawlers", "The Shattered Veil Sanctum", "The Ivalen Kings", "The Madeirna Marauders", "The Tempest Keep Tide", "The Aldenar Roses", "The Gesia Gambit", "The Silphan Minstrels" };
	
    public static List<Character> scoutableFighters = new List<Character>();
    // Use this for initialization
	void Start () {
        if (firstTimeStartup)
        {
            teamNames.Remove(playerTeamName);
            TeamPanelScript tpScript = teamPanel.GetComponent<TeamPanelScript>();
            teamList.Add(CreateATeam(playerTeamName, Character.Faction.friend));
            int rand = 0;
            for (int i = 0; i < 7; i++)
            {
                rand = Random.Range(0, teamNames.Count);
                teamList.Add(CreateATeam((string)teamNames[rand], Character.Faction.enemy));
                teamNames.RemoveAt(rand);
            }
            firstTimeStartup = false;
            foreach (Team t in scheduleOfOpponents)
            {
                print(t.name);
            }
            //populate two schedule lists
            for (int i = 0; i < teamList.Count/2; i++)
            {
                scheduleList1.Add(teamList[i]);
            }
            for (int i = teamList.Count/2; i < teamList.Count; i++)
            {
                scheduleList2.Add(teamList[i]);
            }
            CreateSchedule();

        }
        else
        {
            matchesPlayed++;
            if (matchesPlayed == 60)
            {
                print("NEW SEASON");
                GameObject.Find("LeagueTableButton").GetComponent<LeagueButtonScript>().LeagueButtonOnClick();
            }
        }
        scoutableFighters = GenerateWeeklyScoutableFighters();
        leaguePanel.GetComponentInChildren<LeaguePanelScript>().quickUpdate();
        GameObject.Find("QuickStandings").GetComponent<Text>().text = leaguePanel.GetComponentInChildren<LeaguePanelScript>().playerPosition + " place: " + playerTeamName;
        quickFinances = GameObject.Find("QuickFinances");
        GameObject.Find("QuickFinances").GetComponent<Text>().text = "Funds: " + teamList[0].funds + " GP";
        GameObject.Find("HomePanel").GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Next Match vs \n" + HomeScreenScript.teamList[0].currentOpponentTeam.name;
        NewsFeedPanelScript nfpscript = newsfeedPanel.GetComponentInChildren<NewsFeedPanelScript>();
        nfpscript.GenerateTeamMessages(teamList[0].roster);


    }

    public static void updateQuickFinances() {
        quickFinances.GetComponent<Text>().text = "Funds: " + teamList[0].funds + " GP";
    }

    public static void CreateSchedule()
    {

        for (int i = 0; i < scheduleList1.Count; i++)
        {
            scheduleList1[i].currentOpponentTeam = scheduleList2[i];
        }
    }
    public static void UpdateSchedule()
    {
        Team list1immigrant = scheduleList1[3];
        Team list1carryOver = scheduleList1[2];
        Team list2immigrant = scheduleList2[0];
        scheduleList1.RemoveAt(3);
        scheduleList2.RemoveAt(0);
        scheduleList2.Add(list1immigrant);
        
        for (int i = 1; i < scheduleList1.Count - 1; i++)
        {
            scheduleList1[i + 1] = scheduleList1[i];
        }
        scheduleList1.Add(list1carryOver);

        scheduleList1[1] = list2immigrant;
        for (int i = 0; i < scheduleList1.Count; i++)
        {
            scheduleList1[i].currentOpponentTeam = scheduleList2[i];
            print(scheduleList1[i].name + " is playing " + scheduleList2[i].name);
        }
        
    }

    public static void PlayAIMatches()
    {
        for (int i = 1; i < scheduleList1.Count; i++)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                scheduleList1[i].points += 3;
            }
            else
            {
                scheduleList1[i].currentOpponentTeam.points += 3;
            }
        }
    }

    public static void RestoreRosterCount()
    {
        for (int i = 0; i < teamList.Count; i++)
        {
            if (teamList[i].roster.Count < 10)
            {
                while (teamList[i].roster.Count < 10)
                {
                    if (i == 0)
                    {
                        teamList[i].roster.Add(Character.CreateRandomCharacter(Character.Faction.friend));
                    }
                    else
                    {
                        teamList[i].roster.Add(Character.CreateRandomCharacter(Character.Faction.enemy));
                    }
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Team CreateATeam(string name, Character.Faction faction)
    {
        Team t = new Team();
        t.name = name;
        t.roster = new ArrayList();
        t.points = 0;
        t.funds = 100;
        for (int i = 0; i < 10; i++)
        {
            t.roster.Add(Character.CreateRandomCharacter(faction));
        }
        return t;
    }

    public List<Character> GenerateWeeklyScoutableFighters() {
        List<Character> r = new List<Character>();
        for (int i = 0; i < 3; i++)
        {
            r.Add(Character.CreateRandomCharacter(Character.Faction.friend));
        }
        return r;
    }

    public static void DailyHealing(ArrayList roster)
    {
        for (int i = 0; i < 7; i++)
        {
            foreach (Character c in roster)
            {
                c.DailyHealth();
            }
        }
        
    }

    public static void SetUpcomingOpponent(Team t)
    {
        print("before" + currentOpponentTeamIndex);
        if (currentOpponentTeamIndex >= scheduleOfOpponents.Count - 1)
        {
            currentOpponentTeamIndex = 1;
        }
        else
        {
            currentOpponentTeamIndex++;
        }
        print("after" + currentOpponentTeamIndex);
        currentOpponentTeam = scheduleOfOpponents[currentOpponentTeamIndex];
    }


}





