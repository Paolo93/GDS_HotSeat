using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Joker : Soldier, IJoker
{
    public bool hasAttackBlocked = false;

    public void BlockAttack(Unit enemy)
    {
        if (BlockableAttackUnits().Contains(enemy))
        {
            if(enemy.isAttackBlocked == false && this.cooldown < 1)
            {
                gameManager.ResetTiles();
                gameManager.UpdateStatsPanelLeft();
                gameManager.UpdateStatsPanelRight();
                hasAttackBlocked = true;
                hasMoved = true;
                hasAttacked = true;
                enemy.isAttackBlocked = true;
                enemy.restTurnOfDebuffAttack = 2;
                gameManager.ShowMessage($"Jednostka {this.name} zarzuciła blokadę ataku na jednostkę {enemy.name}");
                this.cooldown += 2;
            }
            else
            {
                gameManager.ShowMessage($"Jednostka {this.name} nie może blokować przez {this.cooldown} tur");
            }
        }
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
