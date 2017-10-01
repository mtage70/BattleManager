using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsFeedPanelScript : MonoBehaviour {
    public GameObject NewsFeedMessagePrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateTeamMessages(ArrayList team)
    {
        foreach(Character c in team)
        {
            CreateMessageForCharacter(c);
        }
        
    }

    public void CreateMessageForCharacter(Character c)
    {
        string message = c.MakeNewsFeedMessage();
        if (message != "")
        {
            GameObject newsfeedmessage = Instantiate(NewsFeedMessagePrefab) as GameObject;
            newsfeedmessage.GetComponent<NewsFeedMessagePrefabScript>().Initialize(c, message);
            newsfeedmessage.transform.SetParent(gameObject.transform, false);
        }
        
    }
}
