﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Soldier : Unit, IAttacker
{
    
    public bool hasAttacked;

    public void Attack(Unit enemy)
    {
        int bonus = 0;
        (string attack, string target) interaction = (this.gameObject.tag, enemy.gameObject.tag);
        if (TagBonuses.bonusDMG.ContainsKey(interaction))
        {
            bonus = TagBonuses.bonusDMG[interaction];
        }

        if (AttackableUnits().Contains(enemy))
        {
            hasAttacked = true;
            hasMoved = true;
            gameManager.ResetTiles();

            int myDamage = attackDamage - enemy.armor + bonus;

            if (Random.Range(0, 100) < chance)
            {
                enemy.health -= myDamage;
                gameManager.ShowMessage($"Jednostka {this.name} atakuje {enemy.name} i zadaje {myDamage} punktów obrażeń");
                FindObjectOfType<AudioManager>().Play("dzwiek_ataku");
            }
            else
            {
                //var miss = Messages.Miss[Random.Range(0, Messages.Miss.Length -1)];
                gameManager.ShowMessage($"{this.name} atakuje {enemy.name} i pudłuje");
                FindObjectOfType<AudioManager>().Play("miss");
            }

            if (enemy.health <= 0)
            {
                DestroyUnit(enemy);
                GetWalkablePaths();
                gameManager.RemoveStatsPanel(enemy);
                gameManager.AddScore(enemy.playerNumber, enemy.value);
                //var kill = Messages.Kill[Random.Range(0, Messages.Kill.Length - 1)];
                gameManager.ShowMessage($"Statek {enemy.name} zostaje zniszczony przez jednostkę {this.name}");
                FindObjectOfType<AudioManager>().Play("smierc-eksplozja");
                if (enemy.isKing == true && playerNumber == 1)
                {
                    gameManager.KingDeath("Dowódca drużyny Szmaragdów pokonany, drużyna Złotych wygrywa!");
                    Time.timeScale = 0f;
                }
                else if (enemy.isKing == true && playerNumber == 2)
                {
                    gameManager.KingDeath("Dowódca drużyny Złotych pokonany, drużyna Szmaragdy wygrywa!");
                    Time.timeScale = 0f;
                }
            }

            if (health <= 0)
            {
                gameManager.ResetTiles();
                DestroyUnit(this);
            }

            if (enemy.health <= (enemy.maxHealth / 2))
            {
                enemy.particleFire.Play();
            }


            gameManager.UpdateStatsPanelLeft();
            gameManager.UpdateStatsPanelRight();
            unitManager.RefreshSelectedUnitTargets();
        }
    }

    public List<Unit> AttackableUnits()
    {
        if (hasAttacked)
        {
            //Debug.Log($"{this.name} has already attacked");
            return new List<Unit>();
        }
        else
        {
            return EnemyUnitsInRange().ToList();
        }
    }

    public void ResetHasAttacked()
    {
        hasAttacked = false;
    }
}
