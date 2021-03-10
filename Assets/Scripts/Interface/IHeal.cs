using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealer
{
    void Heal(Unit target);

    List<Unit> HealableUnits();

    void ResetHasHealed();
}