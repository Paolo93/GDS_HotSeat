using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Joker : Soldier, IJoker
{
    public bool hasAttackBlocked = false;

    public void BlockAttack(Unit enemy)
    {
        var getTurn = gameManager.NumberOfTurn;
        var getNextTurn = gameManager.NumberOfTurn+2;


        if (AttackableUnits().Contains(enemy))
        {
            Debug.Log($"{this.name} blocked {enemy.name}");
            hasAttackBlocked = true;
            hasMoved = true;
            enemy.isAttackBlocked = true;
            gameManager.ResetTiles();
            gameManager.UpdateStatsPanelLeft();
            gameManager.UpdateStatsPanelRight();
        }
        /*
        while(getTurn <= getNextTurn)
        {
            enemy.isAttackBlocked = false;
        }
        */
    }

    public List<Unit> BlockableAttackUnits()
    {
        if (hasAttackBlocked)
        {
            Debug.Log($"{this.name} has already blocked");
            return new List<Unit>();
        }
        else
        {
            return EnemyUnitsInRange().ToList();
        }
    }

    public void ResetHasAttackBlocked()
    {
        hasAttacked = false;
    }

}
