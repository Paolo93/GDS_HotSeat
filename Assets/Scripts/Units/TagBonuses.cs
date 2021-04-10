using System.Collections.Generic;

public static class TagBonuses
{
    public static readonly Dictionary<(string attack, string target), int> bonusDMG = new Dictionary<(string attack, string target), int>(){
    {("porazajacy", "specjalny"), 10},
    {("porazajacy", "podniebny"), 10},
    {("porazajacy", "uziemiajacy"), 0},
    {("porazajacy", "pancerny"), 0},

    {("specjalny", "porazajacy"), 0},
    {("specjalny", "uziemiajacy"), 10},
    {("specjalny", "pancerny"), 0},
    {("specjalny", "przebijajacy"), 10},

    {("podniebny", "porazajacy"), 0},
    {("podniebny", "uziemiajacy"), 10},
    {("podniebny", "pancerny"), 10},
    {("podniebny", "przebijajacy"), 0},

    {("uziemiajacy", "porazajacy"), 10},
    {("uziemiajacy", "specjalny"), 0},
    {("uziemiajacy", "podniebny"), 10},
    {("uziemiajacy", "przebijajacy"), 10},

    {("pancerny", "porazajacy"), 10},
    {("pancerny", "specjalny"), 10},
    {("pancerny", "podniebny"), 0},
    {("pancerny", "przebijajacy"), 0},

    {("przebijajacy", "specjalny"), 0},
    {("przebijajacy", "podniebny"), 10},
    {("przebijajacy", "uziemiajacy"), 0},
    {("przebijajacy", "pancerny"), 10},
  };
}