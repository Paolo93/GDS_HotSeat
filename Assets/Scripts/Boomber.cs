using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Boomber : Soldier, IBoomber
{
    public bool hasMassAtacked = false;

    public void MassAttack(List<Unit> targets)
    {
       
    }


    public List<Unit> MassAtackableUnits()
    {
        if (hasMassAtacked)
        {
            Debug.Log($"{this.name} has already attacked");
            return new List<Unit>();
        }
        else
        {
            return UnitsInRange().ToList();
        }
    }

    public void ResetHasMassAtacked()
    {
        hasMassAtacked = false;
    }

}
