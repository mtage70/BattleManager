
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteMage : Character {
    int healThreshold = 0;
    public int healReserves = 0;
    public int healReservesMax = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Initialize(string first, string last, Gender gender, Profession prof, Faction faction)
    {
        healThreshold = Random.Range(15, 85);
        if (healThreshold < 33)
        {
            traits.Add("Relaxed Healer");
        }
        else if (healThreshold < 66)
        {
            traits.Add("Stoic Healer");
        }
        else
        {
            traits.Add("Anxious Healer");
        }
        healReserves = Random.Range(5, 21);
        if (Random.Range(0, 100) == 26)
        {
            traits.Add("Blessed");
        }
        if (traits.Contains("Blessed"))
        {
            healReserves += wisdom / 10;
        }
        if (healReserves > 20)
        {
            traits.Add("Great Reserves");
        }
        else if (healReserves > 10)
        {
            traits.Add("Moderate Reserves");
        }
        else
        {
            traits.Add("Forsaken");
        }
        healReservesMax = healReserves;
        base.Initialize(first, last, gender, prof, faction);
    }

    public override void AssignPortrait()
    {
        if (this.characterGender == Gender.male)
        {
            portrait = Resources.Load<Sprite>("whitemageIconMale");
        }
        else
        {
            portrait = Resources.Load<Sprite>("whitemageIconFemale");
        }
    }

    public override void WeightStats()
    {
        strength = (int)(NextGaussianDouble() * 10 + 45);
        skill = (int)(NextGaussianDouble() * 10 + 15);
        wisdom = (int)(NextGaussianDouble() * 10 + 75);
        overallRating = wisdom + (strength / 10);
        maximumHealth = 50 + strength / 10;
        currentHealth = maximumHealth;
    }

    public override IEnumerator DecideTurnAction(ArrayList playerCombatants, ArrayList enemyCombatants)
    {
        yield return new WaitForSecondsRealtime(0);
        ArrayList attackableTargets = new ArrayList();
        ArrayList primaryTargets = new ArrayList();
        ArrayList healableTargets = new ArrayList();
        for (int i = 0; i < playerCombatants.Count; i++)
        {
            if (((Character)playerCombatants[i]).isAlive() &&
                ((float)((Character)playerCombatants[i]).currentHealth / (float)((Character)playerCombatants[i]).maximumHealth) * 100 < healThreshold)
            {
                healableTargets.Add(playerCombatants[i]);
            }
        }
        if (healableTargets.Count > 0 && healReserves > 0)
        {
            Debug.Log(healableTargets.Count);
            Heal((Character)healableTargets[Random.Range(0, healableTargets.Count)]);
        }
        else
        {
            for (int i = 0; i < enemyCombatants.Count; i++)
            {
                if (((Character)enemyCombatants[i]).isAlive())
                {
                    attackableTargets.Add(enemyCombatants[i]);
                }
            }
            if (traits.Contains("Warrior Hater"))
            {
                foreach (Character c in attackableTargets)
                {
                    if (c.characterProfession == Profession.warrior)
                    {
                        primaryTargets.Add(c);
                    }
                }
            }
            if (traits.Contains("Rogue Hater"))
            {
                foreach (Character c in attackableTargets)
                {
                    if (c.characterProfession == Profession.rogue)
                    {
                        primaryTargets.Add(c);
                    }
                }
            }
            if (traits.Contains("Black Mage Hater"))
            {
                foreach (Character c in attackableTargets)
                {
                    if (c.characterProfession == Profession.blackmage)
                    {
                        primaryTargets.Add(c);
                    }
                }
            }
            if (traits.Contains("White Mage Hater"))
            {
                foreach (Character c in attackableTargets)
                {
                    if (c.characterProfession == Profession.whitemage)
                    {
                        primaryTargets.Add(c);
                    }
                }
            }

            if (primaryTargets.Count != 0)
            {
                
                BasicAttack((Character)primaryTargets[Random.Range(0, primaryTargets.Count)], 5);
                if (traits.Contains("Frenzied") && CritCheck())
                {
                    BasicAttack((Character)attackableTargets[Random.Range(0, primaryTargets.Count)], 0);
                }
            }
            else
            {
                BasicAttack((Character)attackableTargets[Random.Range(0, attackableTargets.Count)], 0);
                if (traits.Contains("Frenzied") && CritCheck())
                {
                    BasicAttack((Character)attackableTargets[Random.Range(0, attackableTargets.Count)], 0);
                }
            }

        }
        

    }

    public void Heal(Character target)
    {
        int healRoll = Random.Range(0, 11) + wisdom / 10;
        target.currentHealth += healRoll;
        if (target.currentHealth > target.maximumHealth)
        {
            target.currentHealth = target.maximumHealth;
        }
        BattleDirector bd = GameObject.Find("MessagePanel").GetComponentInChildren<BattleDirector>();
        bd.StartCoroutine(bd.MakeMessage(this, "Healed " + target.FullName() + " for " + healRoll + " health!", target));
        target.battleIcon.DisplayHealing(healRoll);
        healReserves--;
        if (healReserves == 0)
        {
            bd.StartCoroutine(bd.MakeMessage(this, FullName() + " has exhausted their healing reserves!", this));
        }
    }

    public override int DefenseRoll()
    {
        return 12;
    }

    public override void DailyHealth()
    {
        currentHealth += Random.Range(1, 16) + wisdom/10;
        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }
    }
}
