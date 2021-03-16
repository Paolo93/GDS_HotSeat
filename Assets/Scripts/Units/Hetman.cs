using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Hetman : Soldier, IHetman
{
    public bool hasMoveBlocked = false;

    public void BlockMove(Unit enemy)
    {
        var getTurn = gameManager.NumberOfTurn;
        var getNextTurn = gameManager.NumberOfTurn + 2;


        if (AttackableUnits().Contains(enemy))
        {
            Debug.Log($"{this.name} blocked {enemy.name}");
            hasMoveBlocked = true;
            hasMoved = true;
            enemy.isMoveBlocked = true;
            gameManager.ResetTiles();
            gameManager.UpdateStatsPanelLeft();
            gameManager.UpdateStatsPanelRight();
        }
        /*
        while(getTurn <= getNextTurn)
        {
            enemy.isMoveBlocked = false;
        }
        */
    }

    public List<Unit> BlockableMoveUnits()
    {
        if (hasMoveBlocked)
        {
            Debug.Log($"{this.name} has already blocked");
            return new List<Unit>();
        }
        else
        {
            return EnemyUnitsInRange().ToList();
        }
    }

    public void ResetHasMoveBlocked()
    {
        hasAttacked = false;
    }
}
