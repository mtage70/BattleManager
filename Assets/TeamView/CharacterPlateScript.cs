using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterPlateScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    public string firstName;
    public string lastName;
    public Sprite portrait;
    public Character.Profession profession;
    Button characterPanelButton;
    public GameObject characterSheetPrefab;
    public Character character;
    public bool selectable = false;
    public bool selected = false;
    Color defaultColor;
    bool holding = false;
    float timeMouseDown = 0;

    public bool purchasable;
    

    public void Initialize (Character pc, Button characterPanelButton, bool selectable, bool purchasable = false)
    {
        character = pc;
        firstName = character.firstName;
        lastName = character.lastName;
        portrait = character.portrait;
        profession = character.characterProfession;
        this.characterPanelButton = characterPanelButton;
        this.characterPanelButton.onClick.AddListener(CharacterPanelButtonOnClick);
        this.selectable = selectable;
        defaultColor = gameObject.GetComponent<Image>().color;
        this.purchasable = purchasable;
    }

    public void Initialize (string first, string last, Sprite port, Character.Profession prof, Button characterPanelButton)
    {
        firstName = first;
        lastName = last;
        portrait = port;
        profession = prof;
        this.characterPanelButton = characterPanelButton;
        this.characterPanelButton.onClick.AddListener(CharacterPanelButtonOnClick);
        defaultColor = gameObject.GetComponent<Image>().color;

    }

    

    // Use this for initialization
    void Start () {
        gameObject.GetComponentInChildren<Slider>().maxValue = character.maximumHealth;
        gameObject.GetComponentInChildren<Slider>().value = character.currentHealth;
        gameObject.GetComponentInChildren<Text>().text = "" + firstName + " " + lastName;
        gameObject.GetComponentsInChildren<Image>()[1].sprite = portrait;
        if (selectable) {
            gameObject.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Button>().enabled = true;
            gameObject.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Button>().onClick.AddListener(PortraitButtonOnClick);
        }
        else {
            gameObject.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Button>().enabled = false;
        }
        switch (profession)
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
                gameObject.GetComponentsInChildren<Text>()[1].text = profession.ToString().ToUpper();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (holding)
        // {
        //     timeMouseDown += Time.deltaTime;
        //     if (timeMouseDown >= 1)
        //     {
        //         holding = false;
        //         timeMouseDown = 0;
        //         GameObject characterSheetPopup = Instantiate(characterSheetPrefab) as GameObject;
        //         characterSheetPopup.GetComponent<CharacterSheetScript>().Initialize(character);
        //         if (selectable)
        //         {
        //             characterSheetPopup.transform.SetParent(GameObject.Find("LineupPanel").transform, false);
        //         }
        //         else
        //         {
        //             characterSheetPopup.transform.SetParent(GameObject.Find("TeamPanel").transform, false);
        //         }

        //     }
        //     print(timeMouseDown);
        // }
    }

    void PortraitButtonOnClick() {
        GameObject characterSheetPopup = Instantiate(characterSheetPrefab) as GameObject;
        characterSheetPopup.GetComponent<CharacterSheetScript>().Initialize(character, true);
        characterSheetPopup.transform.SetParent(GameObject.Find("LineupPanel").transform, false);
    }

    public static Canvas getCanvas(GameObject g)
    {
        if (g.GetComponent<Canvas>() != null)
        {
            return g.GetComponent<Canvas>();
        }
        else
        {
            if (g.transform.parent != null)
            {
                return getCanvas(g.transform.parent.gameObject);
            }
            return null;
        }
    }

    public void CharacterPanelButtonOnClick ()
    {
        holding = false;
        timeMouseDown = 0;
        print("I'm a character named " + firstName + " " + lastName);
        if (selectable)
        {
            if (selected)
            {
                gameObject.GetComponent<Image>().color = defaultColor;
                LineupPanelScript.chosenCount--;
                LineupPanelScript.chosenRoster.Remove(character);
                selected = false;
            }
            else if (LineupPanelScript.chosenCount < 5)
            {
                selected = true;
                gameObject.GetComponent<Image>().color = UnityEngine.Color.green;
                if (LineupPanelScript.chosenCount != 5)
                {
                    LineupPanelScript.chosenRoster.Add(character);
                    LineupPanelScript.chosenCount++;
                }
            }
            
                
        }
        else
        {
            GameObject characterSheetPopup = Instantiate(characterSheetPrefab) as GameObject;
            print(purchasable);
            characterSheetPopup.GetComponent<CharacterSheetScript>().Initialize(character, purchasable);
            characterSheetPopup.transform.SetParent(GameObject.Find("TeamPanel").transform, false);
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holding = true;
        print("holding");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        holding = false;
        timeMouseDown = 0;
    }
}
