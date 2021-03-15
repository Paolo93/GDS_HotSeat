
using System.Collections.Generic;

public interface IHetman
{
    void BlockMove(Unit target);

    List<Unit> BlockableMoveUnits();

    void ResetHasMoveBlocked();
}
