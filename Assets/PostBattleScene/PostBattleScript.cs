using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostBattleScript : MonoBehaviour {
    public static ArrayList battleCasualties = new ArrayList();
    public GameObject obidPrefab;
	// Use this for initialization
	void Start () {
        battleCasualties.Clear();
        GetDeadCombatants();
        CreateObids();
	}


    public static void GetDeadCombatants()
    {
        foreach (Character c in BattleDirector.playerCombatants)
        {
            if (c.currentHealth <= -14)
            {
                print(c.FullName() + " is dead!");
                PostBattleScript.battleCasualties.Add(c);
                HomeScreenScript.deadCharacters.Add(c);
                HomeScreenScript.teamList[0].roster.Remove(c);
            }
        }
        foreach (Character c in BattleDirector.enemyCombatants)
        {
            if (c.currentHealth <= -14)
            {
                print(c.FullName() + " is dead!");
                PostBattleScript.battleCasualties.Add(c);
                HomeScreenScript.deadCharacters.Add(c);
                HomeScreenScript.teamList[0].currentOpponentTeam.roster.Remove(c);
            }
        }
    }

    public void CreateObids()
    {
        foreach (Character c in battleCasualties)
        {
            GameObject obid = Instantiate(obidPrefab) as GameObject;
            obid.GetComponent<ObidPrefabScript>().Initialize(c);
            obid.transform.SetParent(gameObject.transform, false);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
