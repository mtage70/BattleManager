using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSheetPurchaseButtonScript : MonoBehaviour {
    public Button purchaseButton;

	public GameObject CharacterSheetPrefab;

	// Use this for initialization
	void Start () {
		Button pButton = purchaseButton.GetComponent<Button>();
        purchaseButton.onClick.AddListener(PurchaseButtonOnClick);
        purchaseButton.GetComponent<Image>().color = UnityEngine.Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void PurchaseButtonOnClick()
    {
		if (HomeScreenScript.teamList[0].funds >= CharacterSheetPrefab.GetComponent<CharacterSheetScript>().character.value) {
			HomeScreenScript.teamList[0].roster.Add(CharacterSheetPrefab.GetComponentInChildren<CharacterSheetScript>().character);
			HomeScreenScript.teamList[0].funds -= CharacterSheetPrefab.GetComponent<CharacterSheetScript>().character.value;
			HomeScreenScript.scoutableFighters.Remove(CharacterSheetPrefab.GetComponentInChildren<CharacterSheetScript>().character);
			HomeScreenScript.updateQuickFinances();
			GameObject.Find("TeamPanel").GetComponentInChildren<TeamPanelScript>().formatScoutableFightersForTeamPanel();
			GameObject.DestroyObject(CharacterSheetPrefab);
		}
		else {
			purchaseButton.GetComponent<Image>().color = UnityEngine.Color.red;
		}
	}
}
