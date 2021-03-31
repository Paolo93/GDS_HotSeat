using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    

    public GameObject statsPanel, statsPanel2;
    private Unit activeUnit;

    public int scorePlayerOne = 0, scorePlayerTwo = 0;

    //[HideInInspector]
    public int numberOfTurn = 1;
    public int playerTurn = 1;
    [HideInInspector]
    public static bool isDesignMode = false;

    public Text numberOfTurnTxt;
    public Text messageToShow;
    public Text winText;
    [SerializeField] GameObject panelWin;

    [Space(10)]
    public Text scorePlayerOneTxt, scorePlayerTwoTxt;

    public Text unitName, unitName2;
    public Text healthTxt, healthTxt2;
    public Text attackDamageTxt, attackDamageTxt2;
    public Text armorTxt, armorTxt2;
    public Text attackRangeTxt, attackRangeTxt2;
    public Text chanceTxt, chanceTxt2;
    public Text describtion, describtion2;
    public Slider sliderHP, sliderHP2;
    public Text scoreBattle;
    

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
            scorePlayerOneTxt.text = scorePlayerOne.ToString();
        }
        else
        {
            scorePlayerTwo += value;
            scorePlayerTwoTxt.text = scorePlayerTwo.ToString();
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
        if (UnitManager.selectedUnit && UnitManager.selectedUnit.playerNumber == 2 && activeUnit)
        {
            var myAttack = UnitManager.selectedUnit.attackDamage;
            var enemyHealth = activeUnit.health + activeUnit.armor;
            var score = enemyHealth - myAttack;
            Debug.Log(score);
            scoreBattle.text = "HP po ataku bez bonusu: " + score.ToString();
        }
        else
        {
            scoreBattle.text = "Wynik starcia ";
        }
        
        if (activeUnit)
        {
            unitName.text = activeUnit.name;
            healthTxt.text = "Hp " + activeUnit.health.ToString();
            attackDamageTxt.text = activeUnit.attackDamage.ToString();
            armorTxt.text = activeUnit.armor.ToString();
            attackRangeTxt.text = activeUnit.attackRange.ToString();
            chanceTxt.text =  activeUnit.chance.ToString();
            debuffMove.text = "Rest of Turn Debuff Move: " + activeUnit.restTurnOfDebuffMove.ToString();
            debuffAttack.text = "Rest of Turn Debuff Attack: " + activeUnit.restTurnOfDebuffAttack.ToString();
            sliderHP.GetComponent<Slider>().value = activeUnit.health / activeUnit.maxHealth;
        }

    }

    public void ScoreUnits()
    {

    }

    public void UpdateStatsPanelRight()
    {
        if (UnitManager.selectedUnit && UnitManager.selectedUnit.playerNumber == 1 && activeUnit)
        {
            var myAttack = UnitManager.selectedUnit.attackDamage;
            var enemyHealth = activeUnit.health + activeUnit.armor;
            var score = enemyHealth - myAttack;
            Debug.Log(score);
            scoreBattle.text = "HP po ataku bez bonusu: " + score.ToString();
        } else
        {
            scoreBattle.text = "Wynik starcia ";
        }
        if (activeUnit)
        {
            unitName2.text = activeUnit.name;
            healthTxt2.text = "Hp " + activeUnit.health.ToString();
            attackDamageTxt2.text = activeUnit.attackDamage.ToString();
            armorTxt2.text = activeUnit.armor.ToString();
            attackRangeTxt2.text =  activeUnit.attackRange.ToString();
            chanceTxt2.text =  activeUnit.chance.ToString();
            debuffMove2.text = "Rest of Turn Debuff Move: " + activeUnit.restTurnOfDebuffMove.ToString();
            debuffAttack2.text = "Rest of Turn Debuff Attack: " + activeUnit.restTurnOfDebuffAttack.ToString();
            sliderHP2.GetComponent<Slider>().value = activeUnit.health / activeUnit.maxHealth;
        }
    }

    public void UpdateTurn()
    {
        numberOfTurn++;
        numberOfTurnTxt.text = numberOfTurn.ToString();
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
            ShowMessage($"zmiana tury na Szmaragdy");
            scoreBattle.text = "Wynik starcia ";
        }
        else if (playerTurn == 2)
        {
            playerTurn = 1;
            ShowMessage($"zmiana tury na Zlotych");
            UpdateTurn();
            scoreBattle.text = "Wynik starcia ";
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
            ShowMessage($"Zloci Wygrali");
            KingDeath("Zloci Wygrali");
        }
        else if(scorePlayerOne < scorePlayerTwo)
        {
            ShowMessage($"Szmaragdzi Wygrali");
            KingDeath("Szmaragdzi Wygrali");
        }
        else
        {
            ShowMessage($"Remis");
            KingDeath("Remis");
        }
        panelWin.SetActive(true);
    }

    public void KingDeath(string winnerTeam)
    {
        winText.text = winnerTeam.ToString();
        panelWin.SetActive(true);
    }

    public void ChangeDesignMode()
    {
        if (!isDesignMode)
        {
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                if(units.designIcon) units.designIcon.SetActive(true);
                units.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            isDesignMode = true;
        }
        else if(isDesignMode)
        {
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                if (units.designIcon) units.designIcon.SetActive(false);
                units.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            isDesignMode = false;
        }
    }

}
