using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{

    private SpriteRenderer spriteRend;
    public LayerMask obstacles;

    public float hoverAmount;
    public Color colorMove;
    private bool isWalkable;
    GameManager gameManager;


    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }


    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * hoverAmount;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * hoverAmount;
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
        spriteRend.color = Color.white;
        isWalkable = false;
    }

    private void OnMouseDown()
    {
        if(isWalkable && gameManager.selectedUnit != null)
        {
            gameManager.selectedUnit.Move(this.transform.position);
        }
    }
}
