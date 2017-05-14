using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObidPrefabScript : MonoBehaviour {

    public Character character;
	// Use this for initialization
	void Start () {
        gameObject.GetComponentsInChildren<Text>()[0].text = character.FullName();
        gameObject.GetComponentsInChildren<Image>()[1].sprite = character.portrait;
        if (character.characterFaction == Character.Faction.enemy)
        {
            gameObject.GetComponentsInChildren<Text>()[0].color = UnityEngine.Color.red;
        }
        else
        {
            gameObject.GetComponentsInChildren<Text>()[0].color = UnityEngine.Color.green;
        }
        switch (character.characterProfession)
        {
            case Character.Profession.warrior:
                gameObject.GetComponentsInChildren<Text>()[1].text = character.overallRating + " WAR";
                break;
            case Character.Profession.rogue:
                gameObject.GetComponentsInChildren<Text>()[1].text = character.overallRating + " ROG";
                break;
            case Character.Profession.blackmage:
                gameObject.GetComponentsInChildren<Text>()[1].text = character.overallRating + " BLM";
                break;
            case Character.Profession.whitemage:
                gameObject.GetComponentsInChildren<Text>()[1].text = character.overallRating + " WHM";
                break;

            default:
                gameObject.GetComponentsInChildren<Text>()[1].text = character.characterProfession.ToString().ToUpper();
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(Character c)
    {
        character = c;
    }
}
