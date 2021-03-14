using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Joker : Soldier, IJoker
{
    public bool hasBlocked = false;

    public void Block(Unit enemy)
    {
        var getTurn = gameManager.NumberOfTurn;
        var getNextTurn = gameManager.NumberOfTurn+2;


        if (AttackableUnits().Contains(enemy))
        {
            Debug.Log($"{this.name} blocked {enemy.name}");
            hasBlocked = true;
            hasMoved = true;
            enemy.isBlocked = true;
            gameManager.ResetTiles();
            gameManager.UpdateStatsPanel();
        }
        /*
        if (getTurn >= getNextTurn)
        {
            enemy.isBlocked = false;
        }
        */
    }

    public List<Unit> BlockableUnits()
    {
        if (hasBlocked)
        {
            Debug.Log($"{this.name} has already blocked");
            return new List<Unit>();
        }
        else
        {
            return EnemyUnitsInRange().ToList();
        }
    }

    public void ResetHasBlocked()
    {
        hasAttacked = false;
    }

}
