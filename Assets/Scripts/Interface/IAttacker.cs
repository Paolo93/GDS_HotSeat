using System.Collections.Generic;

public interface IAttacker
{
    void Attack(Unit target);

    List<Unit> AttackableUnits();

    void ResetHasAttacked();
}
