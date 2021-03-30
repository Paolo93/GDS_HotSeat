using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hospital : Unit, IHealer
{
	public int healAmount = 5;

	public bool hasHealed = false;

    public void Heal(Unit target)
	{
		if (HealableUnits().Contains(target))
		{
			Debug.Log($"Jednostka {target.name} został naprawiona");
			hasHealed = true;
			hasMoved = true;
			gameManager.ResetTiles();
			target.health += healAmount;
            gameManager.UpdateStatsPanelLeft();
            gameManager.UpdateStatsPanelRight();
        }
	}

	public List<Unit> HealableUnits()
	{
		if (hasHealed)
		{
			Debug.Log($"{this.name} has already healed");
			return new List<Unit>();
		}
		else
		{
			return FriendlyUnitsInRange().ToList();
		}
	}

	public void ResetHasHealed()
	{
		hasHealed = false;
	}
}