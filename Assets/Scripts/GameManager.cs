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

    [Space(10)]
    public Text scorePlayerOneTxt, scorePlayerTwoTxt;

    public Text healthTxt, healthTxt2;
    public Text attackDamageTxt, attackDamageTxt2;
    public Text armorTxt, armorTxt2;
    public Text attackRangeTxt, attackRangeTxt2;
    public Text chanceTxt, chanceTxt2;

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
        player = activeUnit.playerNumber;
        if(player == 1)
        {
            scorePlayerOne += value;
        }
        else
        {
            scorePlayerTwo += value;
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
                UpdateStatsPanel();
            }
            else
            {
                statsPanel2.SetActive(true);
                UpdateStatsPanel2();
            }
        }
        else
        {
            statsPanel.SetActive(false);
            statsPanel2.SetActive(false);
            activeUnit = null;
        }
    }
   
    public void UpdateStatsPanel()
    {
         healthTxt.text = "Hp " + activeUnit.health.ToString();
         attackDamageTxt.text = "Dmg " + activeUnit.attackDamage.ToString();
         armorTxt.text = "Armor " + activeUnit.armor.ToString();
         attackRangeTxt.text = "Range " + activeUnit.attackRange.ToString();
         chanceTxt.text = "Chance " + activeUnit.chance.ToString();
    }

    public void UpdateStatsPanel2()
    {
         healthTxt2.text = "Hp " + activeUnit.health.ToString();
         attackDamageTxt2.text = "Dmg " + activeUnit.attackDamage.ToString();
         armorTxt2.text = "Armor " + activeUnit.armor.ToString();
         attackRangeTxt2.text = "Range " + activeUnit.attackRange.ToString();
         chanceTxt2.text = "Chance " + activeUnit.chance.ToString();
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
        }
        else if (playerTurn == 2)
        {
            playerTurn = 1;
            UpdateTurn();
        }

        UnitManager.DeselectUnit();

        ResetTiles();

        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.ResetState();
            Unit.DisableAttackIcon();
        }
    }
}
