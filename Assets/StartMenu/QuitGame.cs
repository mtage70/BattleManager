using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class QuitGame : MonoBehaviour {

    public Button yourButton;
	// Use this for initialization
	void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick ()
    {
        File.Delete(Application.persistentDataPath + "/teamList.sav");
        File.Delete(Application.persistentDataPath + "/schedule1.sav");
        File.Delete(Application.persistentDataPath + "/schedule2.sav");


    }
    
}
