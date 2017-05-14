using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamPlateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Initialize(Team t, int index)
    {
        gameObject.GetComponentsInChildren<Text>()[0].text = "<b>" + ReferenceMaterial.AddOrdinal(index) + "</b> " + t.name;
        gameObject.GetComponentsInChildren<Text>()[1].text = t.points.ToString();

    }
}
