using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Character {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void AssignPortrait()
    {
        if (this.characterGender == Gender.male)
        {
            portrait = Resources.Load<Sprite>("rogueIconMale");
        }
        else
        {
            portrait = Resources.Load<Sprite>("rogueIconFemale");
        }
    }

    public override void WeightStats()
    {
        strength = (int)(NextGaussianDouble() * 10 + 45);
        skill = (int)(NextGaussianDouble() * 10 + 75);
        wisdom = (int)(NextGaussianDouble() * 10 + 15);
        overallRating = skill + (strength / 10);
        currentHealth = 50;
        maximumHealth = 50;
    }

    public override int DefenseRoll()
    {
        return 12 + (skill / 10);
    }

    public override void DailyHealth()
    {

        currentHealth += UnityEngine.Random.Range(0, 16) + wisdom / 10;
        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
    }
}
