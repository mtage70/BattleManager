using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEventScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(Character actor, string message, Character target)
    {
        gameObject.GetComponentsInChildren<Image>()[1].sprite = actor.portrait;
        gameObject.GetComponentsInChildren<Image>()[2].sprite = target.portrait;
        gameObject.GetComponentInChildren<Text>().text = message;
        if (actor.characterFaction == Character.Faction.friend)
        {
            gameObject.GetComponentInChildren<Text>().color = Color.green;
        }
        else if (actor.characterFaction == Character.Faction.enemy)
        {
            gameObject.GetComponentInChildren<Text>().color = Color.red;
        }
        else
        {
            gameObject.GetComponentInChildren<Text>().color = Color.black;
        }
    }
}
