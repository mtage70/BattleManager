using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraitEntryScript : MonoBehaviour {
    Button btn;
    bool active = false;
    string traitKey;
    string traitValue;
    float height;
    // Use this for initialization
    void Start () {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TraitButtonOnClick);
        height = btn.image.rectTransform.sizeDelta.y;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(string trait)
    {
        traitKey = trait;
        GetComponentInChildren<Text>().text = trait;
        if (ReferenceMaterial.traitsDictionary.ContainsKey(traitKey))
        {
            ReferenceMaterial.traitsDictionary.TryGetValue(traitKey, out traitValue);
        }
        else
        {
            ReferenceMaterial.healerTraitsDictionary.TryGetValue(traitKey, out traitValue);
        }
    }

    public void TraitButtonOnClick()
    {
        Canvas.ForceUpdateCanvases();
        if (active)
        {
            btn.image.rectTransform.sizeDelta = new Vector2(btn.image.rectTransform.sizeDelta.x, height);
            GetComponentInChildren<Text>().text = traitKey;
            active = false;
        }
        else if (ReferenceMaterial.traitsDictionary.ContainsKey(traitKey))
        {
            active = true;
            btn.image.rectTransform.sizeDelta = new Vector2(btn.image.rectTransform.sizeDelta.x, btn.image.rectTransform.sizeDelta.y * 5);
            GetComponentInChildren<Text>().text += "\n" + traitValue;
            print(GetComponentInChildren<Text>().text);
        }
        else
        {
            btn.image.rectTransform.sizeDelta = new Vector2(btn.image.rectTransform.sizeDelta.x, btn.image.rectTransform.sizeDelta.y * 5);
            active = true;
            GetComponentInChildren<Text>().text += "\n" + traitValue;
            print(GetComponentInChildren<Text>().text);
        }
    }
}
