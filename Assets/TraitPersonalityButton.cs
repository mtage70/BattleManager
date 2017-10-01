using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitPersonalityButton : MonoBehaviour {
    Button tButton;
    private GameObject traitContent;
    private GameObject personalityContent;
    private bool traitsOn = true;
	// Use this for initialization
	void Start () {
        traitContent = GameObject.Find("TraitsContent");
        personalityContent = GameObject.Find("PersonalityContent");
        tButton = gameObject.GetComponent<Button>();
        tButton.onClick.AddListener(TraitPersonalityButtonOnClick);
        gameObject.GetComponentInChildren<Text>().text = "Traits";
        personalityContent.SetActive(false);
        traitContent.SetActive(true);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TraitPersonalityButtonOnClick()
    {
        if (traitsOn)
        {
            traitsOn = false;
            gameObject.GetComponentInChildren<Text>().text = "Personality";
            personalityContent.SetActive(true);
            traitContent.SetActive(false);
        }
        else
        {
            traitsOn = true;
            gameObject.GetComponentInChildren<Text>().text = "Traits";
            personalityContent.SetActive(false);
            traitContent.SetActive(true);
        }
    }
}
