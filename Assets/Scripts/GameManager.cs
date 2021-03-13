using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerTurn = 1;

    public GameObject statsPanel;
   
    private Unit activeUnit;

    public int numberOfTurn = 0;

    public Text numberOfTurnTxt;

    public Text healthTxt;
    public Text attackDamageTxt;
    public Text armorTxt;
    public Text attackRangeTxt, chanceTxt;
 
    private UnitManager UnitManager;

    public int NumberOfTurn
    {
        get
        {
            return numberOfTurn;
        }
    }

    private void Start()
    {
        UnitManager = FindObjectOfType<UnitManager>();
    }

    public void ShowStatsPanel(Unit unit)
    {
        if (unit != activeUnit)
        {
            statsPanel.SetActive(true);
            
            activeUnit = unit;
            UpdateStatsPanel();
            Debug.Log("test");
        }
        else
        {
            statsPanel.SetActive(false);
            activeUnit = null;
        }
    }

    public void UpdateTurn()
    {
        numberOfTurn++;
        numberOfTurnTxt.text = "Turn: " + numberOfTurn.ToString();
    }

    public void UpdateStatsPanel()
    {
        healthTxt.text = activeUnit.health.ToString();
        attackDamageTxt.text = activeUnit.attackDamage.ToString();
        armorTxt.text = activeUnit.armor.ToString();
        attackRangeTxt.text = activeUnit.attackRange.ToString();
        chanceTxt.text = activeUnit.chance.ToString();
    }

    public void RemoveStatsPanel(Unit unit)
    {
        if (unit.Equals(activeUnit))
        {
            statsPanel.SetActive(false);
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
