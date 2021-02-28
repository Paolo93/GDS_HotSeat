using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Unit : MonoBehaviour
{

    //[HideInInspector]
    public bool hasMoved, selected, hasAttacked;

    [Tooltip("Amount of tiles to walk")] public int tileAmount;
    [Tooltip("Speed of unit")] public float moveSpeed;
   
    public int playerNumber;
    public int attackRange;

    List<Unit> enemiesInRange = new List<Unit>();

    [Space(10)]
    [Header("Unit Stats Battle")]
    
    public int health;
    public int attackDamage;
    public int defenseDamage;
    public int armor;

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
        }
    }

    private void OnMouseDown()
    {
        DisableAttackIcon();

        if (selected)//ready to select
        {
            selected = false;
            gameManager.selectedUnit = null;
            gameManager.ResetTiles();
            Debug.Log("isSelected = true");
        }
        else
        {
            if(playerNumber == gameManager.playerTurn)
            {
                if (gameManager.selectedUnit != null)
                {
                    gameManager.selectedUnit.selected = false;
                    Debug.Log("gameManager.selectedUnit != null");
                }
                selected = true;
                gameManager.selectedUnit = this;
                gameManager.ResetTiles();
                GetEnemies();
                GetWalkablePaths();
            }
        }

        Collider2D collider = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = collider.GetComponent<Unit>();
        if(gameManager.selectedUnit != null)
        {
            if(gameManager.selectedUnit.enemiesInRange.Contains(unit) && gameManager.selectedUnit.hasAttacked == false)
            {
                gameManager.selectedUnit.Attack(unit);
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
            { 
                if (tile.isClear() == true)
                { 
                    tile.SetCanMove();
                }
            }
        }
    }

    void Attack(Unit enemy)
    {
        hasAttacked = true;
        int enemyDamage = attackDamage - enemy.armor;
        int myDamage = enemy.defenseDamage - armor;

        if (enemyDamage >= 1)
        {
            enemy.health -= enemyDamage;
        }

        if (myDamage >= 1)
        {
            health -= myDamage;
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

    public void Move(Vector2 tilePosition)
    {
        gameManager.ResetTiles();
        float distance = Vector2.Distance(transform.position, tilePosition);

        Sequence moveUnitSequence = DOTween.Sequence();
        moveUnitSequence.Append(transform.DOMoveX(tilePosition.x, distance / moveSpeed));
        moveUnitSequence.Append(transform.DOMoveY(tilePosition.y, distance / moveSpeed));

        hasMoved = true;
        DisableAttackIcon();
        GetEnemies();
        gameManager.ShiftStatsPanel(this);
    }
}
