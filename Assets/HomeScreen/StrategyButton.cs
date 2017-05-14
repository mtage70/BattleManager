using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyButton : MonoBehaviour {

    public Button button;
    public GameObject homePanel;
    public GameObject strategyPanel;
	// Use this for initialization
	void Start () {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OpenStrategyPanel);

    }

    // Update is called once per frame
    void Update () {

    }

    public void OpenStrategyPanel()
    {
        homePanel.SetActive(false);
        strategyPanel.SetActive(true);

    }
}
