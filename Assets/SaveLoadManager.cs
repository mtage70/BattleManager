using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager{

	public static void SaveTeamList(List<Team> l) {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath + "/teamList.sav", FileMode.Create);

		TeamListData tldata = new TeamListData(l);

		bf.Serialize(stream, tldata);
		stream.Close();
	}

	public static void SaveSchedule1() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath + "/schedule1.sav", FileMode.Create);

		TeamListData schedule1 = new TeamListData(HomeScreenScript.scheduleList1);

		bf.Serialize(stream, schedule1);
		stream.Close();
	}
	public static void SaveSchedule2() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath + "/schedule2.sav", FileMode.Create);

		TeamListData schedule2 = new TeamListData(HomeScreenScript.scheduleList2);

		bf.Serialize(stream, schedule2);
		stream.Close();
	}

	public static void LoadSchedule1() {
		if (File.Exists(Application.persistentDataPath + "/schedule1.sav")){
			List<Team> teamlist = new List<Team>();
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath + "/schedule1.sav", FileMode.Open);

			TeamListData schedule1 = (TeamListData) bf.Deserialize(stream);
			stream.Close();
			foreach(TeamData td in schedule1.tdlist ) {
				Debug.Log(schedule1.tdlist);
				teamlist.Add(new Team().CreateSpecificTeam(td));
				ArrayList roster = new ArrayList();
				foreach(CharacterData cd in td.cldata) {
					roster.Add(Character.CreateSpecificCharacter(cd));
				}
			}
			HomeScreenScript.scheduleList1 = teamlist;
		}
	}

	public static void LoadSchedule2() {
		if (File.Exists(Application.persistentDataPath + "/schedule2.sav")){
			List<Team> teamlist = new List<Team>();
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath + "/schedule2.sav", FileMode.Open);

			TeamListData schedule2 = (TeamListData) bf.Deserialize(stream);
			stream.Close();
			foreach(TeamData td in schedule2.tdlist ) {
				Debug.Log(schedule2.tdlist);
				teamlist.Add(new Team().CreateSpecificTeam(td));
				ArrayList roster = new ArrayList();
				foreach(CharacterData cd in td.cldata) {
					roster.Add(Character.CreateSpecificCharacter(cd));
				}
			}
			HomeScreenScript.scheduleList2 = teamlist;
		}
		
	}
	public static List<Team> LoadTeamList() {
		if (File.Exists(Application.persistentDataPath + "/teamList.sav")){
			List<Team> teamlist = new List<Team>();
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath + "/teamList.sav", FileMode.Open);
			
			TeamListData tldata = (TeamListData) bf.Deserialize(stream);
			
			stream.Close();
			foreach(TeamData td in tldata.tdlist ) {
				teamlist.Add(new Team().CreateSpecificTeam(td));
				ArrayList roster = new ArrayList();
				foreach(CharacterData cd in td.cldata) {
					roster.Add(Character.CreateSpecificCharacter(cd));
				}
			}
			return teamlist;
		}
		else {
			return new List<Team>();
		}
	}
}

[Serializable]
public class TeamListData {
	public List<TeamData> tdlist = new List<TeamData>();
	public TeamListData(List<Team> l) {
		foreach(Team t in l) {
			tdlist.Add(new TeamData(t));
		}
	}
}

[Serializable]
public class TeamData {
	public string name;
	public int points;
	public int matchesPlayed;
	public int funds;
	public TeamListData scheduleOfOpponents;
	public List<CharacterData> cldata = new List<CharacterData>();
	public TeamData(Team t) {
		name = t.name;
		points = t.points;
		matchesPlayed = t.matchesPlayed;
		funds = t.funds;
		foreach(Character c in t.roster) {
			cldata.Add(new CharacterData(c));
		}
	}
}

[Serializable]
public class CharacterData {
	public string firstName;
    public string lastName;
    public Character.Gender characterGender;
    public Character.Profession characterProfession;
    public Character.Faction characterFaction;
    public int strength;
    public int skill;
    public int wisdom;
    public int overallRating;
    public int currentHealth;
    public int maximumHealth;
	public ArrayList traits;
	public ArrayList personalities;

	public int healThreshold;
    public int healReserves;
    public int healReservesMax;

	public CharacterData(Character c) {
		firstName = c.firstName;
		lastName = c.lastName;
		characterGender = c.characterGender;
		characterProfession = c.characterProfession;
		characterFaction = c.characterFaction;
		strength = c.strength;
		skill = c.skill;
		wisdom = c.wisdom;
		overallRating = c.overallRating;
		currentHealth = c.currentHealth;
		maximumHealth = c.maximumHealth;
		traits = c.traits;
		personalities = c.personalities;
		if (characterProfession == Character.Profession.whitemage) {
			healThreshold = ((WhiteMage)c).healThreshold;
			healReserves = ((WhiteMage)c).healReserves;
			healReservesMax = ((WhiteMage)c).healReservesMax;
		}
	}
}