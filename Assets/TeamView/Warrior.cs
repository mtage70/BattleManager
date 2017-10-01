using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character {

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
            portrait = Resources.Load<Sprite>("warriorIconMale");
        }
        else
        {
            portrait = Resources.Load<Sprite>("warriorIconFemale");
        }
    }

    public override void WeightStats()
    {
        strength = (int)(NextGaussianDouble() * 10 + 75);
        skill = (int)(NextGaussianDouble() * 10 + 45);
        wisdom = (int)(NextGaussianDouble() * 10 + 15);
        overallRating = strength + (skill / 10);
        maximumHealth = 75;
        currentHealth = 75;
    }

    public override int DefenseRoll()
    {
        return 14;
    }

    public override void DailyHealth()
    {

        currentHealth += UnityEngine.Random.Range(1, 16) + wisdom / 10;
        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
    }
}
