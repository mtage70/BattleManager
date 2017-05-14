using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleDirector : MonoBehaviour {
    public static ArrayList playerCombatants = LineupPanelScript.chosenRoster;
    public static ArrayList enemyCombatants = new ArrayList() { };
    public GameObject characterIconPrefab;
    public GameObject enemyCombatantsPanel;
    public GameObject playerCombatantsPanel;
    public GameObject messagePanel;
    public GameObject battleEventPrefab;
    public int speed = 1;
    public Button simBtn;
    // Use this for initialization
    void Start() {
        simBtn.onClick.AddListener(SimulateButtonOnClick);

        FormEnemyTeam(HomeScreenScript.teamList[0].currentOpponentTeam.roster);
        ReportEnemyCombatants();
        ReportPlayerCombatants();
        RestoreHealReserves();
        StartCoroutine(PlayIntroMessages());
    }

    // Update is called once per frame
    void Update() {

    }

    public void SimulateButtonOnClick()
    {
        speed = 0;
    }

    public void RestoreHealReserves()
    {
        foreach (Character c in playerCombatants)
        {
            if (c.characterProfession == Character.Profession.whitemage)
            {
                ((WhiteMage)c).healReserves = ((WhiteMage)c).healReservesMax;
            }
        }
    }

    public void FormEnemyTeam(ArrayList enemyRoster)
    {
        int randomIndex;
        for (int i = 0; i < 5; i++)
        {
            randomIndex = Random.Range(0, enemyRoster.Count);
            if (!enemyCombatants.Contains(enemyRoster[randomIndex]))
            {
                enemyCombatants.Add(enemyRoster[randomIndex]);
            }
            else
            {
                i--;
            }
            //enemyRoster.RemoveAt(randomIndex);
        }
    }

    public void ReportEnemyCombatants()
    {
        for (int i = 0; i < enemyCombatants.Count; i++)
        {
            Character c = enemyCombatants[i] as Character;
            GameObject characterIcon = Instantiate(characterIconPrefab) as GameObject;
            characterIcon.GetComponent<CharacterIconScript>().Initialize(c);
            c.battleIcon = characterIcon.GetComponent<CharacterIconScript>();
            characterIcon.transform.SetParent(enemyCombatantsPanel.transform, false);
        }
    }

    public void ReportPlayerCombatants()
    {
        for (int i = 0; i < playerCombatants.Count; i++)
        {
            Character c = playerCombatants[i] as Character;
            GameObject characterIcon = Instantiate(characterIconPrefab) as GameObject;
            characterIcon.GetComponent<CharacterIconScript>().Initialize(c);
            c.battleIcon = characterIcon.GetComponent<CharacterIconScript>();
            characterIcon.transform.SetParent(playerCombatantsPanel.transform, false);
        }
    }

    public IEnumerator MakeMessage(Character actor, string message, Character target)
    {
        yield return new WaitForSecondsRealtime(0);
        GameObject battlEvent = Instantiate(battleEventPrefab) as GameObject;
        battlEvent.GetComponent<BattleEventScript>().Initialize(actor, message, target);
        battlEvent.transform.SetParent(gameObject.transform, false);
        Canvas.ForceUpdateCanvases();
        GameObject.Find("Scroll View").GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    IEnumerator PlayIntroMessages()
    {
        print("PlayIntroMessages()");
        Character pc0 = playerCombatants[0] as Character;
        Character ec0 = enemyCombatants[0] as Character;
        StartCoroutine(MakeMessage(pc0, HomeScreenScript.teamList[0].name + " vs " + HomeScreenScript.teamList[0].currentOpponentTeam.name, ec0));
        yield return new WaitForSecondsRealtime(speed);
        StartCoroutine(MakeMessage(pc0, "Both teams have been assembled!", ec0));
        yield return new WaitForSecondsRealtime(speed);
        StartCoroutine(MakeMessage(pc0, "Battle comencing in 3...", ec0));
        yield return new WaitForSecondsRealtime(speed);
        StartCoroutine(MakeMessage(pc0, "2...", ec0));
        yield return new WaitForSecondsRealtime(speed);
        StartCoroutine(MakeMessage(pc0, "1...", ec0));
        yield return new WaitForSecondsRealtime(speed);
        StartCoroutine(MakeMessage(pc0, "FIGHT!", ec0));
        yield return new WaitForSecondsRealtime(speed);
        StartCoroutine(BattleLoop());

    }

    IEnumerator Wait()
    {
        print(Time.time);
        yield return new WaitForSecondsRealtime(5);
        print(Time.time);
    }

    public bool enemyCombatantsAlive()
    {
        for(int i = 0; i < enemyCombatants.Count; i++)
        {
            if (((Character)enemyCombatants[i]).isAlive())
            {
                return true;
            }
        }
        return false;
    }

    public bool playerCombatantsAlive()
    {
        for (int i = 0; i < playerCombatants.Count; i++)
        {
            if (((Character)playerCombatants[i]).isAlive())
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator BattleLoop()
    {
        ArrayList initiativeOrder = new ArrayList();
        initiativeOrder.AddRange(playerCombatants);
        initiativeOrder.AddRange(enemyCombatants);
        for (int i = 0; i < initiativeOrder.Count; i++)
        {
            ((Character)initiativeOrder[i]).rollForInitiative();
        }
        InitiativeComparer initiativeCompare = new InitiativeComparer();
        initiativeOrder.Sort(initiativeCompare);
        while (playerCombatantsAlive() && enemyCombatantsAlive())
        {
            for (int i = 0; i < initiativeOrder.Count; i++)
            {
                Character currentTurnHolder = ((Character)initiativeOrder[i]);
                if (playerCombatantsAlive() && enemyCombatantsAlive())
                {
                    if (currentTurnHolder.isAlive())
                    {
                        currentTurnHolder.battleIcon.ActivateTurnGlow();
                        StartCoroutine(MakeMessage(currentTurnHolder, "It's " + currentTurnHolder.FullName() + "'s turn!", currentTurnHolder));
                        yield return new WaitForSecondsRealtime(speed);
                        //enemy attacking player combatants
                        if (currentTurnHolder.characterFaction == Character.Faction.enemy)
                        {
                            StartCoroutine(currentTurnHolder.DecideTurnAction(enemyCombatants, playerCombatants));
                        }
                        //friendly attacking enemy combatants
                        else
                        {
                            StartCoroutine(currentTurnHolder.DecideTurnAction(playerCombatants, enemyCombatants));
                        }
                        yield return new WaitForSecondsRealtime(speed);
                    }
                    currentTurnHolder.battleIcon.DeactivateTurnGlow();

                }
                else
                {
                    break;
                }
            }
        }
        StartCoroutine(BattleFinished());
        yield return new WaitForSecondsRealtime(speed);
    }

    IEnumerator BattleFinished()
    {
        if (playerCombatantsAlive())
        {
            StartCoroutine(MakeMessage((Character)playerCombatants[0], "Player wins!", (Character)playerCombatants[0]));
            HomeScreenScript.teamList[0].points += 3;
        }
        else
        {
            StartCoroutine(MakeMessage((Character)enemyCombatants[0], "Enemy wins!", (Character)enemyCombatants[0]));
            HomeScreenScript.teamList[0].currentOpponentTeam.points += 3;
        }
        yield return new WaitForSecondsRealtime(4 + speed);
        speed = 1;
        SceneManager.LoadScene("PostBattleScene");
    }
}
