using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Soldier : Unit, IAttacker
{
    public int attackDamage;
    public int chance;
    public bool hasAttacked;

    public void Attack(Unit enemy)
    {
        if (AttackableUnits().Contains(enemy))
        {
            hasAttacked = true;
            hasMoved = true;
            gameManager.ResetTiles();

            int myDamage = attackDamage - enemy.armor;

            if (Random.Range(0, 100) < chance)
            {
                enemy.health -= myDamage;
                Debug.Log($"{this.name} hit {enemy.name}");
            }
            else
            {
                Debug.Log($"{this.name} missed {enemy.name}");
            }

            if (enemy.health <= 0)
            {
                DestroyUnit(enemy);
                GetWalkablePaths();
            }

            if (health <= 0)
            {
                gameManager.ResetTiles();
                DestroyUnit(this);
            }

            gameManager.UpdateStatsPanel();
            unitManager.RefreshSelectedUnitTargets();
        }
    }

    public List<Unit> AttackableUnits()
    {
        if (hasAttacked)
        {
            Debug.Log($"{this.name} has already attacked");
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
