using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Unit selectedUnit;

    public int playerTurn = 1;
    
    public void ResetTiles()
    {
        foreach(Tiles tile in FindObjectsOfType<Tiles>())
        {
            tile.Reset();
        }
    }

    public void Switch()
    {
        if(playerTurn == 1)
        {
            playerTurn = 2;
        } else if(playerTurn == 2)
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
        }
    }
}
