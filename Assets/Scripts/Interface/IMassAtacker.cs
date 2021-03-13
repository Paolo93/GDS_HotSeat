using System.Collections.Generic;

public interface IMassAtacker
{
    void MassAttack(Unit target);

    List<Unit> MassAttackableUnits();

    void ResetHasMassAttacked();
}
