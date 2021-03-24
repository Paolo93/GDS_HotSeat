using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    

    public GameObject statsPanel, statsPanel2;
    private Unit activeUnit;

    public int scorePlayerOne = 0, scorePlayerTwo = 0;

    [HideInInspector]
    public int numberOfTurn = 0;
    public int playerTurn = 1;

    public Text numberOfTurnTxt;
    public Text messageToShow;

    [Space(10)]
    public Text scorePlayerOneTxt, scorePlayerTwoTxt;

    public Text healthTxt, healthTxt2;
    public Text attackDamageTxt, attackDamageTxt2;
    public Text armorTxt, armorTxt2;
    public Text attackRangeTxt, attackRangeTxt2;
    public Text chanceTxt, chanceTxt2;

    public Text debuffMove, debuffMove2;
    public Text debuffAttack, debuffAttack2;

    private UnitManager UnitManager;

    private void Start()
    {
        UnitManager = FindObjectOfType<UnitManager>();
    }

    public int NumberOfTurn
    {
        get
        {
            return numberOfTurn;
        }
    }

    //add score after kill enemy
    public void AddScore(int player, int value)
    {
        player = UnitManager.selectedUnit.playerNumber;
        if(player == 1)
        {
            scorePlayerOne += value;
            scorePlayerOneTxt.text = "Score P1: " + scorePlayerOne.ToString();
        }
        else
        {
            scorePlayerTwo += value;
            scorePlayerTwoTxt.text = "Score P2: " + scorePlayerTwo.ToString();
        }
    }

    public void ShowStatsPanel(Unit unit)
    {
        if (unit != activeUnit)
        {
            activeUnit = unit;
            if(activeUnit.playerNumber == 1)
            {
                statsPanel.SetActive(true);
                UpdateStatsPanelLeft();
            }
            else if(activeUnit.playerNumber == 2)
            {
                statsPanel2.SetActive(true);
                UpdateStatsPanelRight();
            }
        }
        else 
        {
            statsPanel.SetActive(false);
            statsPanel2.SetActive(false);
            activeUnit = null;
        }
    }

    public void ShowMessage(string message)
    {
        messageToShow.text = message.ToString();
    }
   
    public void UpdateStatsPanelLeft()
    {
        if (activeUnit)
        {
            healthTxt.text = "Hp " + activeUnit.health.ToString();
            attackDamageTxt.text = "Dmg " + activeUnit.attackDamage.ToString();
            armorTxt.text = "Armor " + activeUnit.armor.ToString();
            attackRangeTxt.text = "Range " + activeUnit.attackRange.ToString();
            chanceTxt.text = "Chance " + activeUnit.chance.ToString();
            debuffMove.text = "Rest of Turn Debuff Move: " + activeUnit.restTurnOfDebuffMove.ToString();
            debuffAttack.text = "Rest of Turn Debuff Attack: " + activeUnit.restTurnOfDebuffAttack.ToString();
        }

    }

    public void UpdateStatsPanelRight()
    {
        if (activeUnit)
        {
            healthTxt2.text = "Hp " + activeUnit.health.ToString();
            attackDamageTxt2.text = "Dmg " + activeUnit.attackDamage.ToString();
            armorTxt2.text = "Armor " + activeUnit.armor.ToString();
            attackRangeTxt2.text = "Range " + activeUnit.attackRange.ToString();
            chanceTxt2.text = "Chance " + activeUnit.chance.ToString();
            debuffMove2.text = "Rest of Turn Debuff Move: " + activeUnit.restTurnOfDebuffMove.ToString();
            debuffAttack2.text = "Rest of Turn Debuff Attack: " + activeUnit.restTurnOfDebuffAttack.ToString();
        }
    }

    public void UpdateTurn()
    {
        numberOfTurn++;
        numberOfTurnTxt.text = "Turn: " + numberOfTurn.ToString();
    }

    public void RemoveStatsPanel(Unit unit)
    {
        if (unit.Equals(activeUnit))
        {
            statsPanel.SetActive(false);
            statsPanel2.SetActive(false);
            activeUnit = null;
        }
    }


    public void ResetTiles()
    {
        foreach(Tiles tile in FindObjectsOfType<Tiles>())
        {
            tile.Reset();
        }
    }

    //Switch Turn
    public void Switch()
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;
            ShowMessage($"Czas na Szmaragdy");
        }
        else if (playerTurn == 2)
        {
            playerTurn = 1;
            ShowMessage($"Zloci Do boju");
            UpdateTurn();
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                if (units.isMoveBlocked)
                {
                    units.restTurnOfDebuffMove -= 1;
                }
                if(units.restTurnOfDebuffMove <= 0)
                {
                    units.isMoveBlocked = false;
                }
                if (units.isAttackBlocked)
                {
                    units.restTurnOfDebuffAttack -= 1;
                }
                if (units.restTurnOfDebuffAttack <= 0)
                {
                    units.isAttackBlocked = false;
                }
                units.cooldown -= 1;
                if (units.cooldown < 0)
                {
                    units.cooldown = 0;
                }
            }
        }

        UnitManager.DeselectUnit();
        ResetTiles();
        

        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.ResetState();
            Unit.DisableAttackIcon();
        }
    }

    public void VictoryScore()
    {
        if(scorePlayerOne > scorePlayerTwo)
        {
            //win team 1
        } else if(scorePlayerOne < scorePlayerTwo)
        {
            // win team 2
        } else
        {
            //remis
        }
    }
}
