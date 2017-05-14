using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIconScript : MonoBehaviour {
    Character character;
    float timer = 0;
    bool timing = false;
    public GameObject characterSheetPrefab;
    public static bool openSheet = false;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(CharacterIconOnClick);
        GetComponentInChildren<Text>().text = character.FullName();
        gameObject.GetComponentInChildren<Slider>().maxValue = character.maximumHealth;
        gameObject.GetComponentInChildren<Slider>().value = character.currentHealth;
    }
	
	// Update is called once per frame
	void Update () {
        if (timing)
        {
            timer += Time.deltaTime;
        }
        if (timer > 2)
        {
            timing = false;
            timer = 0;
            gameObject.GetComponentsInChildren<Text>()[1].text = "";
            gameObject.GetComponentsInChildren<Image>()[1].color = Color.clear;
        }
        

        if (character.currentHealth <= 0)
        {
            gameObject.GetComponentInChildren<Image>().color = Color.red;
            gameObject.GetComponentInChildren<Slider>().GetComponentsInChildren<Image>()[1].CrossFadeAlpha(0, 1, true);
        }
        else if (gameObject.GetComponentInChildren<Slider>().value < character.currentHealth)
        {
            gameObject.GetComponentInChildren<Slider>().value += 1;
        }
        else if (gameObject.GetComponentInChildren<Slider>().value > character.currentHealth)
        {
            gameObject.GetComponentInChildren<Slider>().value -= 1;
        }
        
        

    }

    public void Initialize(Character c)
    {
        character = c;
        c.battleIcon = this;
        gameObject.GetComponent<Image>().sprite = c.portrait;
    }

    public void CharacterIconOnClick()
    {
        if (openSheet)
        {
            Object.Destroy(GameObject.Find("CharacterSheetPrefab(Clone)"));
            openSheet = false;
        }
        else
        {
            openSheet = true;
            GameObject characterSheetPopup = Instantiate(characterSheetPrefab) as GameObject;
            characterSheetPopup.GetComponent<CharacterSheetScript>().Initialize(character);
            characterSheetPopup.transform.SetParent(GameObject.Find("MessagePanel").transform, false);
        }
        
    }

    public void ActivateTurnGlow()
    {
        print("activateglow");
        gameObject.GetComponentsInChildren<Image>()[1].color = Color.yellow;
    }

    public void DeactivateTurnGlow()
    {
        print("deactivateglow");
        gameObject.GetComponentsInChildren<Image>()[1].color = Color.clear;
    }

    public void DisplayDamage(int damageValue)
    {
        gameObject.GetComponentsInChildren<Text>()[1].text = "-" + damageValue.ToString();
        gameObject.GetComponentsInChildren<Image>()[1].color = Color.red;
        gameObject.GetComponentsInChildren<Text>()[1].color = Color.red;
        timing = true;
        timer = 0;
    }

    public void DisplayHealing(int healingValue)
    {
        gameObject.GetComponentsInChildren<Text>()[1].text = "+" + healingValue.ToString();
        gameObject.GetComponentsInChildren<Image>()[1].color = Color.green;
        gameObject.GetComponentsInChildren<Text>()[1].color = Color.green;

        timing = true;
        timer = 0;
    }
}
