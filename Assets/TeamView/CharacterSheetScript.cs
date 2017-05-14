using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheetScript : MonoBehaviour {
    Button closeButton;
    public GameObject traitEntryPrefab;
	// Use this for initialization
	void Start () {
        closeButton = gameObject.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(CloseCharacterSheet);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(string name, Sprite portrait, string profession)
    {
        gameObject.GetComponentsInChildren<Text>()[0].text = name;
        gameObject.GetComponentInChildren<Image>().sprite = portrait;
        gameObject.GetComponentsInChildren<Text>()[1].text = profession;
    }
    public void Initialize(Character character)
    {
        gameObject.GetComponentsInChildren<Text>()[0].text = character.FullName();
        gameObject.GetComponentsInChildren<Image>()[2].sprite = character.portrait;
        gameObject.GetComponentsInChildren<Text>()[1].text = "<b>" + character.overallRating + "</b>" + " " + character.characterProfession.ToString().ToUpper();
        GameObject.Find("StrengthVal").GetComponent<Text>().text = character.strength.ToString();
        GameObject.Find("SkillVal").GetComponent<Text>().text = character.skill.ToString();
        GameObject.Find("WisdomVal").GetComponent<Text>().text = character.wisdom.ToString();

        switch (character.characterProfession)
        {
            case Character.Profession.warrior :
                GameObject.Find("StrengthVal").GetComponent<Text>().fontStyle = FontStyle.Bold;
                GameObject.Find("StrengthVal").GetComponent<Text>().fontSize = 20;
                break;
            case Character.Profession.rogue :
                GameObject.Find("SkillVal").GetComponent<Text>().fontStyle = FontStyle.Bold;
                GameObject.Find("SkillVal").GetComponent<Text>().fontSize = 20;
                break;
            case Character.Profession.blackmage:
                GameObject.Find("WisdomVal").GetComponent<Text>().fontStyle = FontStyle.Bold;
                GameObject.Find("WisdomVal").GetComponent<Text>().fontSize = 20;
                break;
            case Character.Profession.whitemage:
                GameObject.Find("WisdomVal").GetComponent<Text>().fontStyle = FontStyle.Bold;
                GameObject.Find("WisdomVal").GetComponent<Text>().fontSize = 20;
                break;
            default: break;
        }
        foreach (string trait in character.traits)
        {
            GameObject traitEntry = Instantiate(traitEntryPrefab) as GameObject;
            traitEntry.GetComponent<TraitEntryScript>().Initialize(trait);
            traitEntry.transform.SetParent(GameObject.Find("TraitsContent").transform, false);
        }



    }

    public void CloseCharacterSheet()
    {
        Object.Destroy(gameObject);
        CharacterIconScript.openSheet = false;
    }
}
