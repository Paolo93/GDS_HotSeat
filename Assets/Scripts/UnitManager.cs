using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Camera camera;

    public Unit selectedUnit;
    public List<Unit> TargetableUnits = new List<Unit>();
    private GameManager GameManager;

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        selectedUnit = null;
    }

    public void DeselectUnit()
    {
        Unit.DisableAttackIcon();
        GameManager.ResetTiles();
        ClearTargetableUnits();
        if(selectedUnit != null)
        {
            Debug.Log($"deselecting unit {selectedUnit.name}");
            selectedUnit = null;
        }
    }

    private void ClearTargetableUnits()
    {
        TargetableUnits.ForEach(x => x.AttackIcon.SetActive(false));
        TargetableUnits.Clear();
    }

    public void SelectUnit(Unit unit)
    {
        Debug.Log($"Selected: {unit.name}");

        if(selectedUnit != null) { DeselectUnit(); }

        ClearTargetableUnits();

        if(unit != null)
        {
            selectedUnit = unit;
            RefreshSelectedUnitTargets();
        }
    }

    public void RefreshSelectedUnitTargets()
    {
        ClearTargetableUnits();
        selectedUnit.GetWalkablePaths();

        if (selectedUnit is IAttacker attacker)
        {
            TargetableUnits.AddRange(attacker.AttackableUnits());
        }

        if (selectedUnit is IHealer healer)
        {
            TargetableUnits.AddRange(healer.HealableUnits());
        }

        TargetableUnits.ForEach(x => x.AttackIcon.SetActive(true));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("LMB clicked");
            var clickedUnit = GetNewClickedUnit();

            if (selectedUnit != null && selectedUnit == clickedUnit) //we clicked on already selected unit
            {
                Debug.Log("selected unit clicked");
                DeselectUnit();
            }
            else
            {
                if (clickedUnit == null) //we didn't click anything
                {
                    Debug.Log("we didn't click any Unit");
                    return;
                }
                if (selectedUnit == null) //select unit
                {
                    if (clickedUnit.playerNumber == GameManager.playerTurn)
                    {
                        SelectUnit(clickedUnit);
                    }
                }
                else //choose who to attack
                {
                    if (clickedUnit.playerNumber == GameManager.playerTurn)
                    {
                        Debug.Log($"selecting different unit, from {selectedUnit.name} to {clickedUnit.name}");
                        SelectUnit(clickedUnit);
                    }
                    else
                    {
                        Debug.Log($"{selectedUnit.name} attempt attack on {clickedUnit.name}");
                        Attack(clickedUnit);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1)) //special Attack
        {
            Debug.Log("RMB clicked");
            CastSpecialAttack();
        }
    }

    private void Attack(Unit clickedUnit)
    {
        if (selectedUnit is IAttacker attacker)
        {
            attacker.Attack(clickedUnit);
        }
    }

    private void CastSpecialAttack()
    {
        if (selectedUnit != null)
        {
            Unit.DisableAttackIcon();
            var clickedUnit = GetNewClickedUnit();
            if (selectedUnit is IHealer healer)
            {
                healer.Heal(clickedUnit);
            }
            if (selectedUnit is IJoker joker)
            {
                joker.Block(clickedUnit);
            }
        }
    }

    private static Unit GetNewClickedUnit()
    {
        Unit clickedUnit = null;

        Collider2D collider = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.20f);

        if (collider != null)
        {
            clickedUnit = collider.GetComponent<Unit>();
            string log = $"clicked {collider.name}";
            if (clickedUnit != null)
            {
                log += $" player: {clickedUnit.playerNumber}";
            }
            Debug.Log(log);
        }
        else
        {
            clickedUnit = null;
        }

        return clickedUnit;
    }
 
}
