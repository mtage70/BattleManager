using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMage : Character {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void AssignPortrait()
    {
        portrait = Resources.Load<Sprite>("blackmageIcon");
    }

    public override void WeightStats()
    {
        strength = (int)(NextGaussianDouble() * 10 + 15);
        skill = (int)(NextGaussianDouble() * 10 + 45);
        wisdom = (int)(NextGaussianDouble() * 10 + 75);
        overallRating = wisdom + (skill / 10);
        maximumHealth = 50;
        currentHealth = 50;
    }

    public override int DefenseRoll()
    {
        return 10;
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
