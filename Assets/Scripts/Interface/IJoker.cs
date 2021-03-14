using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJoker
{
    void Block(Unit target);

    List<Unit> BlockableUnits();

    void ResetHasBlocked();
}