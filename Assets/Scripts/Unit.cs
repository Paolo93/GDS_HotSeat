using System.Collections;
using System.Collections.Generic;
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
   // public int defenseDamage;
    public int armor;
    public int chance;

    private GameManager gameManager;
    public GameObject AttackIcon;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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

    private void GetWalkablePaths()
    {
        if (hasMoved) return; 

        Tiles[] tiles = FindObjectsOfType<Tiles>();
        foreach (Tiles tile in tiles)
        {
            if (TileInRange(tile) && tile.isClear())
            { // how far he can move
                if (tile.isClear() == true)
                {
                    tile.SetCanMove();
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
        //int enemyDamage = attackDamage - enemy.armor;
        //int myDamage = enemy.defenseDamage - armor;

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
/*
        if (myDamage >= 1)
        {
            health -= myDamage;
        }
*/
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

    public void DisableAttackIcon()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.AttackIcon.SetActive(false);
        }
    }

    private bool TileInRange(Tiles tile)
    {
        return Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileAmount;
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

}
