using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Hetman : Soldier, IHetman
{
    public bool hasMoveBlocked = false;

    public void BlockMove(Unit enemy)
    {
        if (BlockableMoveUnits().Contains(enemy))
        {    
            if(enemy.isMoveBlocked == false && this.cooldown < 1)
            {
                gameManager.ResetTiles();
                gameManager.UpdateStatsPanelLeft();
                gameManager.UpdateStatsPanelRight();
                hasMoveBlocked = true;
                hasMoved = true;
                hasAttacked = true;
                enemy.isMoveBlocked = true;
                enemy.restTurnOfDebuffMove = 2;
                gameManager.ShowMessage($"Jednostka {this.name} zarzuciła blokadę ruchu na jednostkę {enemy.name}");
                this.cooldown += 2;
            } 
            else
            {
                gameManager.ShowMessage($"Jednostka {this.name} nie może blokować przez {this.cooldown} tur");
            }
        }
    }

    public List<Unit> BlockableMoveUnits()
    {
        if (hasMoveBlocked)
        {
           // Debug.Log($"{this.name} has already blocked");
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
