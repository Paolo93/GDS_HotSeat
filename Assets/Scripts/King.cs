using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class King : Unit, IHealer
{
	public int healAmount = 5;


	public bool hasHealed = false;
	
	public void Heal(Unit target)
	{
		if (HealableUnits().Contains(target))
		{
			Debug.Log($"{this.name} healed {target.name}");
			hasHealed = true;
			hasMoved = true;
			gameManager.ResetTiles();
			target.health += healAmount;
			gameManager.UpdateStatsPanel();
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