using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public bool selected;
    public int tileSpeed;
    public bool hasMoved;

    public float moveSpeed;

    GameManager gameManager;

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
            if (Mathf.Abs(transform.position.x - tile.transform.position.x) + Mathf.Abs(transform.position.y - tile.transform.position.y) <= tileSpeed)
            { // how far he can move
                if (tile.isClear() == true)
                { // is the tile clear from any obstacles
                    tile.CanMove();
                }
            }
        }
    }

    public void Move(Vector2 tilePosition)
    {
        gameManager.ResetTiles();
        StartCoroutine(StartMovement(tilePosition));
    }

    IEnumerator StartMovement(Vector2 tilePosition)
    {
        while(transform.position.x != tilePosition.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePosition.x, transform.position.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        while (transform.position.y != tilePosition.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePosition.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        hasMoved = true;
    }
}
