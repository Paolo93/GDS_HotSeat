using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    //[HideInInspector]
    public bool hasMoved;
    //[HideInInspector]
    public bool isAttackBlocked = false;
    //[HideInInspector]
    public bool isMoveBlocked = false;
    [HideInInspector]
    public int restTurnOfDebuffMove, restTurnOfDebuffAttack;
    [HideInInspector]
    public float maxHealth;


    public bool isKing;
    public int cooldown;

    public string name;
    public int playerNumber;
    
    [Space(10)]
    [Header("Stats for Designers")]
    [Tooltip("Amount of tiles to walk")] public int tileAmount;
    [Tooltip("Speed of unit")] public float moveSpeed;
    public int attackDamage;
    public int chance;
    
    public int health;
    public int armor;
    public int attackRange;
    public int value;

    protected GameManager gameManager;
    protected UnitManager unitManager;
    public GameObject AttackIcon;
    public GameObject designIcon;

    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        unitManager = FindObjectOfType<UnitManager>();
        maxHealth = health;

    }

    public void GetWalkablePaths()
    {
        if (hasMoved)
        {
            //Debug.Log($"Unit {this.name} already moved");     
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
        if (!isMoveBlocked)
        {
            gameManager.ResetTiles();
            float distance = Vector2.Distance(transform.position, tilePosition.position);

            //var move = Messages.Move[Random.Range(0, Messages.Move.Length - 1)];
            gameManager.ShowMessage($"Jednostka {this.name} rusza się");

            
            Sequence moveUnitSequence = DOTween.Sequence();
            moveUnitSequence.Append(transform.DOMoveX(tilePosition.position.x, distance / moveSpeed));
            moveUnitSequence.Append(transform.DOMoveY(tilePosition.position.y, distance / moveSpeed));
            moveUnitSequence.AppendCallback(() =>
            {
                hasMoved = true;
                unitManager.RefreshSelectedUnitTargets();
                FindObjectOfType<AudioManager>().Play("ruch_jednostki");
            });

        DisableAttackIcon();
        
        }
        else
        {
            gameManager.ShowMessage($"Jednostka {this.name} ma chwilowo zablokowany ruch");
        }
    }

    public void OnMouseEnter()
    {
        gameManager.ShowStatsPanel(this);
    }

    public void OnMouseExit()
    {
        gameManager.RemoveStatsPanel(this);
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
        if (this is IJoker joker)
        {
            joker.ResetHasAttackBlocked();
        }
        if (this is IHetman hetman)
        {
            hetman.ResetHasMoveBlocked();
        }
        if (this is IBoomber boomber)
        {
            boomber.ResetHasMassAtacked();
        }
    }
    /*
    public void MoveUnitName()
    {
        if(unitNameTxt) unitNameTxt.transform.position = (Vector2)this.transform.position + new Vector2(0, 0.5f);
    }
    */

}
