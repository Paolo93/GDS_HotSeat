using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Unit : MonoBehaviour
{

    [HideInInspector]
    public bool hasMoved, selected, hasAttacked;

    public bool isKing;
    private int hpPoint = 5;

    [Tooltip("Amount of tiles to walk")] public int tileAmount;
    [Tooltip("Speed of unit")] public float moveSpeed;
   
    public int playerNumber;
    public int attackRange;
    public int value;

    List<Unit> enemiesInRange = new List<Unit>();
    List<Unit> myUnitsInRange = new List<Unit>();
    

    [Space(10)]
    [Header("Unit Stats Battle")]
    
    public int health;
    public int attackDamage;
   
    public int armor;
    public int chance;

    private GameManager gameManager;
    public GameObject AttackIcon;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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

    private IEnumerable<Unit> UnitsInRange()
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
        moveUnitSequence.AppendCallback(() => hasMoved = true);
        if (!isKing) { moveUnitSequence.AppendCallback(() => GetEnemies()); }


        DisableAttackIcon();

        gameManager.ShiftStatsPanel(this);
    }


    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            gameManager.ShowStatsPanel(this);
 
            Collider2D collider = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
            Unit unit = collider.GetComponent<Unit>();
            DisableAttackIcon();
            if (gameManager.selectedUnit != null && gameManager.selectedUnit.isKing == true)
            {
                if (gameManager.selectedUnit.myUnitsInRange.Contains(unit) && gameManager.selectedUnit.hasAttacked == false)
                {
                    gameManager.selectedUnit.Heal(unit);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        DisableAttackIcon();

        if (selected) // if currently unit is selected
        {
            selected = false;
            gameManager.selectedUnit = null;
            gameManager.ResetTiles();
        }
        else
        {
            if (playerNumber == gameManager.playerTurn) // if our turn
            {
                if (gameManager.selectedUnit != null)// safe
                {
                    gameManager.selectedUnit.selected = false;
                }
                if (isKing)
                {
                    GetMyUnits();
                    Debug.Log("King");
                }
                else
                {
                    GetEnemies(); 
                    Debug.Log("Unit");
                }
                selected = true;
                gameManager.selectedUnit = this;
                gameManager.ResetTiles();
                GetWalkablePaths();
            }
        }

        Collider2D collider = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = collider.GetComponent<Unit>(); 

        if (Input.GetMouseButtonDown(0))
        {
            if (gameManager.selectedUnit != null)
            {
                if (gameManager.selectedUnit.enemiesInRange.Contains(unit) && gameManager.selectedUnit.hasAttacked == false && gameManager.selectedUnit.isKing == false)
                {
                    gameManager.selectedUnit.Attack(unit);
                }
            }
        }
    }



    void Heal (Unit unit)
    {
        hasAttacked = true;
        hasMoved = true;
        gameManager.ResetTiles();
        unit.health += hpPoint;
        gameManager.UpdateStatsPanel();
    }

    void Attack(Unit enemy)
    {
        hasAttacked = true;
        hasMoved = true;
        gameManager.ResetTiles();

        int myDamage = attackDamage - enemy.armor;

        if(Random.Range(0, 100) < chance)
        {
            enemy.health -= myDamage;
            Debug.Log("Correct");
        }
        else
        {
            Debug.Log("Miss");
        }

        if (enemy.health <= 0)
        {
            Destroy(enemy.gameObject);
            GetWalkablePaths();
            gameManager.RemoveStatsPanel(enemy);
        }

        if (health <= 0)
        {
            gameManager.ResetTiles();
            gameManager.RemoveStatsPanel(this);
            Destroy(this.gameObject);
        }

        gameManager.UpdateStatsPanel();
        
    }

    void GetEnemies()
    {
        enemiesInRange.Clear();
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            if(Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) <= attackRange)
            {
                if(unit.playerNumber != gameManager.playerTurn && !hasAttacked) // attack only player from another turn
                {
                    enemiesInRange.Add(unit);
                    unit.AttackIcon.SetActive(true);
                }
            }
        }
    }

    void GetMyUnits()
    {
        myUnitsInRange.Clear();
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            if (Mathf.Abs(transform.position.x - unit.transform.position.x) + Mathf.Abs(transform.position.y - unit.transform.position.y) <= attackRange)
            {
                if (unit.playerNumber == gameManager.playerTurn) 
                {
                    myUnitsInRange.Add(unit);
                   //unit.AttackIcon.SetActive(false);
                }
            }
        }
    }


    private bool TileInRange(Tiles tile)
    {
        return Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileAmount;
    }
   


}
