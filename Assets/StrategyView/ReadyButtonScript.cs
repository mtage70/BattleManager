using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyButtonScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(ReadyButtonOnClick);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReadyButtonOnClick()
    {
        if (LineupPanelScript.chosenCount == 5)
        {
            LineupPanelScript.chosenCount = 0;
            SceneManager.LoadScene("BattleScene");
        }
        
    }
}
