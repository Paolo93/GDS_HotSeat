using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{

    private SpriteRenderer spriteRend;
    public Sprite[] tiles;
    public LayerMask obstacles;

    public float hoverAmount;
    public Color colorMove;
    private bool isWalkable;
    GameManager gameManager;


    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        //  int randomTile = Random.Range(0, tiles.Length);
        //spriteRend.sprite = tiles[randomTile];
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

    public bool isClear() // does this tile have an obstacle on it. Yes or No?
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

    public void CanMove()
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
