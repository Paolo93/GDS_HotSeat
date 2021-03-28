using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{

    private SpriteRenderer spriteRend;
    public LayerMask obstacles;

    public float hoverTile;
    public Color colorMove, colorReset;
    private bool isWalkable;
    GameManager gameManager;
    private UnitManager UnitManager;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        UnitManager = FindObjectOfType<UnitManager>();
    }


    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * hoverTile;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * hoverTile;
    }

    public bool isClear() 
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 0.2f, obstacles);
        if (col == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetCanMove()
    {
        spriteRend.color = colorMove;
        isWalkable = true;
    }

    public void Reset()
    {
        spriteRend.color = colorReset;
        isWalkable = false;
    }

    private void OnMouseDown()
    {
        if (isWalkable) // && gameManager.selectedUnit != null
        {
            if (UnitManager.selectedUnit != null && !UnitManager.selectedUnit.hasMoved)
            {
                UnitManager.selectedUnit.Move(this.transform);
            }
        }
    }
}
