using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Unit : MonoBehaviour
{

    public bool selected;
    public int tileSpeed;
    public bool hasMoved;

    public float moveSpeed;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        if(selected)//ready to select
        {
            selected = false;
            gameManager.selectedUnit = null;
            gameManager.ResetTiles();
            Debug.Log("isSelected = true");
        }
        else
        {
            if(gameManager.selectedUnit != null)
            {
                gameManager.selectedUnit.selected = false;
                Debug.Log("gameManager.selectedUnit != null");
            }
            selected = true;
            gameManager.selectedUnit = this;
            gameManager.ResetTiles();
            GetWalkablePaths();
        }
    }

    private void GetWalkablePaths()
    {
        //Jesli jest tura gracza i ma do poruszenia sie
        if (hasMoved) return; // nie chcemy aby sie poruszyl ponownie

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

    private bool TileInRange(Tiles tile)
    {
        return Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed;
    }

    public void Move(Vector2 tilePosition)
    {
        gameManager.ResetTiles();
 
        float distance = Vector2.Distance(transform.position, tilePosition);

        Sequence moveUnitSequence = DOTween.Sequence();
        moveUnitSequence.Append(transform.DOMoveX(tilePosition.x, distance / moveSpeed));
        moveUnitSequence.Append(transform.DOMoveY(tilePosition.y, distance / moveSpeed));
        hasMoved = true;

    }
}
