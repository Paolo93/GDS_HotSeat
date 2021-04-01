using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hospital : Unit, IHealer
{
	public int healAmount = 30;

	public bool hasHealed = false;

    public void Heal(Unit target)
	{
		if (HealableUnits().Contains(target))
		{
			Debug.Log($"Jednostka {target.name} został naprawiona");
            FindObjectOfType<AudioManager>().Play("leczenie");
			hasHealed = true;
			hasMoved = true;
			gameManager.ResetTiles();
            if(target.health < target.maxHealth)
            {
                target.health += healAmount;
            }
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