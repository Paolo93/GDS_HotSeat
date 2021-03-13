using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Unit : MonoBehaviour
{

    //[HideInInspector]
    public bool hasMoved;

    [Tooltip("Amount of tiles to walk")] public int tileAmount;
    [Tooltip("Speed of unit")] public float moveSpeed;

    public int playerNumber;
    public string unitTag;

    [Space(10)]
    [Header("Unit Stats Battle")]
    public int health;
    public int armor;
    public int attackRange;
    
    
    protected GameManager gameManager;
    protected UnitManager unitManager;
    public GameObject AttackIcon;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        unitManager = FindObjectOfType<UnitManager>();
       // this.gameObject.tag = unitTag;
    }

    public void GetWalkablePaths()
    {
        if (hasMoved)
        {
            Debug.Log($"Unit {this.name} already moved");
            return;
        }

        Debug.Log($"Unit {this.name} available move tiles highlighted");

        foreach (Tiles tile in FindObjectsOfType<Tiles>())
        {
            if (TargetInMoveRange(tile.transform) && tile.isClear())
            {
                tile.SetCanMove();
            }
        }
    }

    protected static void DestroyUnit(Unit unit)
    {
        unit.gameManager.RemoveStatsPanel(unit);
        Destroy(unit.gameObject);
    }

    protected IEnumerable<Unit> FriendlyUnitsInRange()
    {
        return UnitsInRange().Where(x => x.playerNumber == gameManager.playerTurn);//select my units
    }

    protected IEnumerable<Unit> EnemyUnitsInRange()
    {
        return UnitsInRange().Where(x => x.playerNumber != gameManager.playerTurn);// select my enemies
    }

    protected IEnumerable<Unit> UnitsInRange()
    {
        return FindObjectsOfType<Unit>().Where(x => TargetInAttackRange(x.transform)); // select general unit
    }

    private bool TargetInAttackRange(Transform target)
    {
        return DistanceToTarget(target) <= attackRange;
    }

    private bool TargetInMoveRange(Transform target)
    {
        return DistanceToTarget(target) <= tileAmount;
    }

    private int DistanceToTarget(Transform target)
    {
        Vector3 unitPos = target.position;
        Vector3 myPos = this.transform.position;
        float xDistance = Mathf.Abs(myPos.x - unitPos.x);
        float yDistance = Mathf.Abs(myPos.y - unitPos.y);
        int distanceToUnit = Mathf.RoundToInt(xDistance + yDistance);
        return distanceToUnit;
    }

    public static void DisableAttackIcon()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.AttackIcon.SetActive(false);
        }
    }

    public void Move(Transform tilePosition)
    {
        gameManager.ResetTiles();
        float distance = Vector2.Distance(transform.position, tilePosition.position);

        Sequence moveUnitSequence = DOTween.Sequence();
        moveUnitSequence.Append(transform.DOMoveX(tilePosition.position.x, distance / moveSpeed));
        moveUnitSequence.Append(transform.DOMoveY(tilePosition.position.y, distance / moveSpeed));
        moveUnitSequence.AppendCallback(() =>
        {
            hasMoved = true;
            unitManager.RefreshSelectedUnitTargets();
        });

        DisableAttackIcon();

        gameManager.ShiftStatsPanel(this);
    }

    public void ResetState()
    {
        hasMoved = false;

        if (this is IHealer healer)
        {
            healer.ResetHasHealed();
        }

        if (this is IAttacker attacker)
        {
            attacker.ResetHasAttacked();
        }
    }

}
