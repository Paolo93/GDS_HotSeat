using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomber : Soldier, IMassAtacker
{
    public bool hasMassAttacked;

    public void MassAttack(Unit targets)
    {
        if (MassAttackableUnits().Contains(targets))
        {
            hasAttacked = true;
            hasMoved = true;
            gameManager.ResetTiles();

            int myDamage = attackDamage - targets.armor;

            if (Random.Range(0, 100) < chance)
            {
                targets.health -= myDamage;
                Debug.Log($"{this.name} hit {targets.name}");
            }
            else
            {
                Debug.Log($"{this.name} missed {targets.name}");
            }

            if (targets.health <= 0)
            {
                DestroyUnit(targets);
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

    public List<Unit> MassAttackableUnits()
    {
        if (hasAttacked)
        {
            Debug.Log($"{this.name} has already attacked");
            return new List<Unit>();
        }
        else
        {
            return UnitsInRange().ToList();
        }
    }

    public void ResetHasMassAttacked()
    {
        hasAttacked = false;
    }


}
