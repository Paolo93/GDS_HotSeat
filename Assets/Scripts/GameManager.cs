using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Unit selectedUnit;

    public int playerTurn = 1;

    public GameObject statsPanel;
    public Vector2 offsetStatsPanel;
    private Unit activeUnit;
    public Text healthTxt;
    public Text attackDamageTxt;
    public Text defenseDamageTxt;
    public Text armorTxt;


    public void ShowStatsPanel(Unit unit)
    {
        if(unit.Equals(activeUnit) == false)
        {
            statsPanel.SetActive(true);
            statsPanel.transform.position = (Vector2)unit.transform.position + offsetStatsPanel;
            activeUnit = unit;
            UpdateStatsPanel();
        } else
        {
            statsPanel.SetActive(false);
            activeUnit = null;
        }
    }

    public void UpdateStatsPanel()
    {
        if(activeUnit != null)
        {
            healthTxt.text = activeUnit.health.ToString();
        }
    }

    public void ShiftStatsPanel(Unit unit)
    {
        if (unit.Equals(activeUnit))
        {
            statsPanel.transform.position = (Vector2)unit.transform.position + offsetStatsPanel;
        }
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
        if(playerTurn == 1)
        {
            playerTurn = 2;
        }
        else if(playerTurn == 2)
        {
            playerTurn = 1;
        }

        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        ResetTiles();

        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.hasMoved = false;
            units.AttackIcon.SetActive(false);
            units.hasAttacked = false;
        }
    }
}
