using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    
    [SerializeField]
    private int hpPoint;

  
    private GameManager gameManager;
    private Unit activeUnit;
    

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        activeUnit = GetComponent<Unit>();
    }
    
    public void Heal(Unit unit)
    {
        unit.health += hpPoint;
    }


}
