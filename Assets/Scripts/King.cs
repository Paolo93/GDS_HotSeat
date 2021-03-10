using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Unit, IHealer
{
    public int healAmount = 5;

    public bool hasHealed = false;

    public void Heal(Unit target)
    {
       //pass
    }

    public List<Unit> HealableUnits()
    {
        if (hasHealed)
        {
            Debug.Log($"{this.name} has already healed");
            return new List<Unit>();
        }
        else
        {
            return FriendlyUnitsInRange().ToList();
        }
    }

    public void ResetHasHealed()
    {
        hasHealed = false;
    }
}
