using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewsFeedButtonScript : MonoBehaviour
{
    public Button newsfeedButton;
    public GameObject newsfeedPanel;
    public GameObject homePanel;
    public GameObject lineupPanel;
    private bool active = false;
    private Color activatedColor = UnityEngine.Color.green;
    private Color deactivatedColor = UnityEngine.Color.white;
    // Use this for initialization
    void Start()
    {
        Button tButton = newsfeedButton.GetComponent<Button>();
        tButton.onClick.AddListener(NewsFeedButtonOnClick);
        newsfeedButton.GetComponent<Image>().color = deactivatedColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NewsFeedButtonOnClick()
    {
        if (active)
        {
            active = false;
            newsfeedButton.GetComponent<Image>().color = deactivatedColor;
            newsfeedPanel.SetActive(false);
            homePanel.SetActive(true);
        }
        else
        {
            active = true;
            newsfeedButton.GetComponent<Image>().color = activatedColor;
            newsfeedPanel.SetActive(true);
            homePanel.SetActive(false);
            lineupPanel.SetActive(false);
            


        }

    }
}
