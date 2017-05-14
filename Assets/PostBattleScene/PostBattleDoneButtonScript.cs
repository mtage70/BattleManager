using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostBattleDoneButtonScript : MonoBehaviour {

    public Button btn;

	// Use this for initialization
	void Start () {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(DoneButtonOnClick);;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void DoneButtonOnClick()
    {
        BattleDirector.enemyCombatants.Clear();
        BattleDirector.playerCombatants.Clear();
        HomeScreenScript.PlayAIMatches();
        for (int i = 0; i < HomeScreenScript.teamList.Count; i++)
        {
            HomeScreenScript.DailyHealing(HomeScreenScript.teamList[i].roster);
        }


        HomeScreenScript.UpdateSchedule();
        
        SceneManager.LoadScene("HomeScreen");
    }
}
