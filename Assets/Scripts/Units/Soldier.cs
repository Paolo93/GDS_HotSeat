using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Soldier : Unit, IAttacker
{
    
    public bool hasAttacked;

    public void Attack(Unit enemy)
    {
        int bonus = 0;
        (string attack, string target) interaction = (this.gameObject.tag, enemy.gameObject.tag);
        if (TagBonuses.bonusDMG.ContainsKey(interaction))
        {
            bonus = TagBonuses.bonusDMG[interaction];
        }

        if (AttackableUnits().Contains(enemy))
        {
            hasAttacked = true;
            hasMoved = true;
            gameManager.ResetTiles();

            int myDamage = attackDamage - enemy.armor + bonus;

            if (Random.Range(0, 100) < chance)
            {
                enemy.health -= myDamage;
                gameManager.ShowMessage($"{enemy.name} Dostał !!");
                //Debug.Log($"{this.name} hit {enemy.name}");
            }
            else
            {
                Debug.Log($"{this.name} missed {enemy.name}");
            }

            if (enemy.health <= 0)
            {
                DestroyUnit(enemy);
                GetWalkablePaths();
                gameManager.RemoveStatsPanel(enemy);
                gameManager.AddScore(enemy.playerNumber, enemy.value);
                gameManager.ShowMessage($"Haa! zabilem {enemy.name}");
                //Debug.Log($"{this.name} killed {enemy.name}");
            }

            if (health <= 0)
            {
                if(isKing == true)
                {
                    //victory
                }
                gameManager.ResetTiles();
                DestroyUnit(this);
            }

            gameManager.UpdateStatsPanelLeft();
            gameManager.UpdateStatsPanelRight();
            unitManager.RefreshSelectedUnitTargets();
        }
    }

    public List<Unit> AttackableUnits()
    {
        if (hasAttacked)
        {
            //gameManager.ShowMessage($"{this.name} has already attacked");
            //Debug.Log($"{this.name} has already attacked");
            return new List<Unit>();
        }
        else
        {
            return EnemyUnitsInRange().ToList();
        }
    }

    public void ResetHasAttacked()
    {
        hasAttacked = false;
    }
}
