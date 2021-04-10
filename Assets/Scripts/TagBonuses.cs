using System.Collections.Generic;

public static class TagBonuses
{
    public static readonly Dictionary<(string attack, string target), int> bonusDMG = new Dictionary<(string attack, string target), int>(){
    {("porazajacy", "specjalny"), 10},
    {("porazajacy", "podniebny"), 0},
    {("porazajacy", "uziemiajacy"), 10},
    {("porazajacy", "pancerny"), 0},

    {("specjalny", "porazajacy"), 0},
    {("specjalny", "uziemiajacy"), 0},
    {("specjalny", "pancerny"), 10},
    {("specjalny", "przebijajacy"), 0},

    {("podniebny", "porazajacy"), 0},
    {("podniebny", "uziemiajacy"), 0},
    {("podniebny", "pancerny"), 10},
    {("podniebny", "przebijajacy"), 0},

    {("uziemiajacy", "porazajacy"), 0},
    {("uziemiajacy", "specjalny"), 0},
    {("uziemiajacy", "podniebny"), 10},
    {("uziemiajacy", "przebijajacy"), 0},

    {("pancerny", "porazajacy"), 0},
    {("pancerny", "specjalny"),0},
    {("pancerny", "podniebny"), 10},
    {("pancerny", "przebijajacy"), 0},

    {("przebijajacy", "specjalny"), 0},
    {("przebijajacy", "podniebny"), 0},
    {("przebijajacy", "uziemiajacy"), 10},
    {("przebijajacy", "pancerny"), 0},
  };
}