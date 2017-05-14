using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public abstract class Character : ScriptableObject {
    public enum Gender { male, female }
    public enum Faction { friend, enemy }
    public enum Profession { warrior, rogue, blackmage, whitemage }
    public ArrayList traits = new ArrayList();
    public CharacterIconScript battleIcon;

    public string firstName;
    public string lastName;
    public Sprite portrait;
    public Gender characterGender;
    public Profession characterProfession;
    public Faction characterFaction;
    public int strength;
    public int skill;
    public int wisdom;
    public int overallRating;
    public int initiativeRoll = 0;
    public int currentHealth = 100;
    public int maximumHealth = 100;
    protected int attackRolld20 = 0;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public virtual void Initialize(string first, string last, Gender gender, Profession prof, Faction faction)
    {
        firstName = first;
        lastName = last;
        characterGender = gender;
        characterProfession = prof;
        characterFaction = faction;
        this.AssignPortrait();
        this.WeightStats();
        this.AssignRandomTraits();
    }

    abstract public void AssignPortrait();

    public abstract void WeightStats();

    public void AssignRandomTraits()
    {
        foreach (object key in ReferenceMaterial.RandomTraits(ReferenceMaterial.traitsDictionary).Take(Random.Range(0, 3)))
        {
            if (!traits.Contains(key))
            {
                traits.Add(key);
            }
        }
    }

    public static double NextGaussianDouble()
    {
        double U, u, v, S;

        do
        {
            u = 2.0 * Random.value - 1.0;
            v = 2.0 * Random.value - 1.0;
            S = u * u + v * v;
        }
        while (S >= 1.0);

        double fac = Mathf.Sqrt((float)(-2.0 * Mathf.Log10((float)S) / S));
        return u * fac;
    }

    public static Character CreateRandomCharacter(Faction faction)
    {
        Character temp;
        string first;
        string last;
        int genderVal = Random.Range(0, 2);
        int professionVal = Random.Range(0, 4);
        if (genderVal == 0)
        {
            first = ReferenceMaterial.firstNamesListMale[Random.Range(0, ReferenceMaterial.firstNamesListMale.Count)] as string;
        }
        else
        {
            first = ReferenceMaterial.firstNamesListFemale[Random.Range(0, ReferenceMaterial.firstNamesListFemale.Count)] as string;
        }

        last = ReferenceMaterial.lastNamesList[Random.Range(0, ReferenceMaterial.lastNamesList.Count)] as string;
        switch (professionVal)
        {
            case 0:
                temp = ScriptableObject.CreateInstance<Warrior>();
                break;
            case 1:
                temp = ScriptableObject.CreateInstance<Rogue>();
                break;
            case 2:
                temp = ScriptableObject.CreateInstance<BlackMage>();
                break;
            case 3:
                temp = ScriptableObject.CreateInstance<WhiteMage>();
                break;
            default:
                temp = ScriptableObject.CreateInstance<Rogue>();
                break;
        }

        first = char.ToUpper(first[0]) + first.Substring(1);
        last = char.ToUpper(last[0]) + last.Substring(1);

        temp.Initialize(first, last, (Character.Gender)genderVal, (Character.Profession)professionVal, faction);
        return temp;

    }

    public void rollForInitiative()
    {
        initiativeRoll = Random.Range(0, 20) + (skill / 20);
    }

    public virtual IEnumerator DecideTurnAction(ArrayList playerCombatants, ArrayList enemyCombatants)
    {
        yield return new WaitForSecondsRealtime(0);
        
        ArrayList attackableTargets = new ArrayList();
        ArrayList primaryTargets = new ArrayList();
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

    public void BasicAttack(Character target, int modifier)
    {
        BattleDirector bd = GameObject.Find("MessagePanel").GetComponentInChildren<BattleDirector>();
        if (AttackRoll(target))
        {
            int damageDealt = DealDamage(target, modifier);
            
            if (CritCheck())
            {
                bd.StartCoroutine(bd.MakeMessage(this, "CRITICAL! " + FullName() + " attacked " + target.FullName() + " for " + damageDealt + " damage!", target));
                //half damage to left and right of target
                if (traits.Contains("Wild Strikes"))
                {
                    int targetIndex = 0;
                    if (characterFaction == Character.Faction.friend)
                    {
                        targetIndex = BattleDirector.enemyCombatants.IndexOf(target);
                    }
                    else
                    {
                        targetIndex = BattleDirector.playerCombatants.IndexOf(target);
                    }
                    Character leftOfTarget;
                    Character rightOfTarget;
                    

                    if (targetIndex > 0 && targetIndex < 4)
                    {
                        if (target.characterFaction == Character.Faction.enemy)
                        {
                            leftOfTarget = ((Character)BattleDirector.enemyCombatants[targetIndex - 1]);
                            rightOfTarget = ((Character)BattleDirector.enemyCombatants[targetIndex + 1]);
                        }
                        else
                        {
                            leftOfTarget = ((Character)BattleDirector.playerCombatants[targetIndex - 1]);
                            rightOfTarget = ((Character)BattleDirector.playerCombatants[targetIndex + 1]);
                        }
                        attackRolld20 = 1;
                        DealDamage(leftOfTarget, modifier);
                        DealDamage(rightOfTarget, modifier);
                        bd.StartCoroutine(bd.MakeMessage(this,
                            leftOfTarget.FullName() + " took " + damageDealt/2 + " damage!",
                            leftOfTarget));
                        bd.StartCoroutine(bd.MakeMessage(this,
                            rightOfTarget.FullName() + " took " + damageDealt / 2 + " damage!",
                            rightOfTarget));
                    }
                    else if (targetIndex > 0)
                    {
                        if (target.characterFaction == Character.Faction.enemy)
                        {
                            leftOfTarget = ((Character)BattleDirector.enemyCombatants[targetIndex - 1]);
                        }
                        else
                        {
                            leftOfTarget = ((Character)BattleDirector.playerCombatants[targetIndex - 1]);
                        }
                        attackRolld20 = 1;
                        DealDamage(leftOfTarget, (damageDealt / 2));
                        bd.StartCoroutine(bd.MakeMessage(this,
                            leftOfTarget.FullName() + " took " + damageDealt / 2 + " damage!",
                            leftOfTarget));
                    }
                    else
                    {
                        if (target.characterFaction == Character.Faction.enemy)
                        {
                            rightOfTarget = ((Character)BattleDirector.enemyCombatants[targetIndex + 1]);
                        }
                        else
                        {
                            rightOfTarget = ((Character)BattleDirector.playerCombatants[targetIndex + 1]);
                        }
                        attackRolld20 = 1;
                        DealDamage(rightOfTarget, (damageDealt / 2));
                        bd.StartCoroutine(bd.MakeMessage(this,
                            rightOfTarget.FullName() + " took " + damageDealt / 2 + " damage!",
                            rightOfTarget));
                    }
                }   
            }
            else
            {
                bd.StartCoroutine(bd.MakeMessage(this, "Attacked " + target.FullName() + " for " + damageDealt + " damage!", target));
            }
            if (target.currentHealth <= 0)
            {
                bd.StartCoroutine(bd.MakeMessage(this, FullName() + " killed " + target.FullName() + "!", target));
            }
        }
        else
        {
            bd.StartCoroutine(bd.MakeMessage(this, "Their attack missed!", target));
        }
    }

    public bool AttackRoll(Character target)
    {
        attackRolld20 = Random.Range(1, 21);
        if (CritCheck())
        {
            return true;
        }
        switch (characterProfession)
        {
            case Profession.warrior:
                return attackRolld20 + strength / 10 > target.DefenseRoll();
            case Profession.rogue:
                return attackRolld20 + skill / 10 > target.DefenseRoll();
            case Profession.blackmage:
                return attackRolld20 + wisdom / 10 > target.DefenseRoll();
            case Profession.whitemage:
                return attackRolld20 + strength / 10 > target.DefenseRoll();
            default:
                return false;

        }
    }

    //defense is based on skill?
    public virtual int DefenseRoll()
    {
        int defenseRolld20 = Random.Range(1, 21);
        return 8 + (skill / 10); //defenseRolld20 + (skill / 10);
    }

    public bool CritCheck() {
        return attackRolld20 == 20;
    }

    public int DealDamage(Character target, int modifier)
    {
        int damage = 0;
        switch (characterProfession)
        {
            case Profession.warrior:
                damage = Random.Range(0, 11) + strength / 10;
                if (CritCheck())
                {
                    damage *= 2;
                }
                target.currentHealth -= damage + modifier;
                target.battleIcon.DisplayDamage(damage + modifier);
                return damage + modifier;
            case Profession.rogue:
                damage = Random.Range(0, 11) + skill / 10;
                if (CritCheck())
                {
                    damage *= 2;
                }
                target.currentHealth -= damage + modifier;
                target.battleIcon.DisplayDamage(damage + modifier);
                return damage + modifier;
            case Profession.blackmage:
                damage = Random.Range(0, 11) + wisdom / 10;
                if (CritCheck())
                {
                    damage *= 2;
                }
                target.currentHealth -= damage + modifier;
                target.battleIcon.DisplayDamage(damage + modifier);
                return damage + modifier;
            case Profession.whitemage:
                damage = Random.Range(0, 11) + strength / 10;
                if (CritCheck())
                {
                    damage *= 2;
                }
                target.currentHealth -= damage + modifier;
                target.battleIcon.DisplayDamage(damage + modifier);
                return damage + modifier;
            default:
                return 0;
        }
    }

    public string FullName ()
    {
        return this.firstName + " " + this.lastName;
    }

    public bool isAlive()
    {
        if (currentHealth > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract void DailyHealth();
}
