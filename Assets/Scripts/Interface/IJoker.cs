using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJoker
{
    void BlockAttack(Unit target);

    List<Unit> BlockableAttackUnits();

    void ResetHasAttackBlocked();
}