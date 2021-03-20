
using System.Collections.Generic;


public interface IBoomber
{
    void MassAttack(List<Unit> targets);

    List<Unit> MassAtackableUnits();

    void ResetHasMassAtacked();

}
