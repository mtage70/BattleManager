using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsFeedMessagePrefabScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(Character actor, string message)
    {
        gameObject.GetComponentsInChildren<Image>()[0].sprite = actor.portrait;
        gameObject.GetComponentInChildren<Text>().text = message;
		gameObject.GetComponentsInChildren<Text>()[1].text = actor.FullName();
    }
}
