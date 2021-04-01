using System.Collections.Generic;

public static class TagBonuses
{
    public static readonly Dictionary<(string attack, string target), int> bonusDMG = new Dictionary<(string attack, string target), int>(){
    {("porazajacy", "specjalny"), 10},
    {("porazajacy", "podniebny"),-10},
    {("porazajacy", "uziemiajacy"), 10},
    {("porazajacy", "pancerny"), -10},

    {("specjalny", "porazajacy"), -10},
    {("specjalny", "uziemiajacy"),-10},
    {("specjalny", "pancerny"), 10},
    {("specjalny", "przebijajacy"), -10},

    {("podniebny", "porazajacy"), -10},
    {("podniebny", "uziemiajacy"),-10},
    {("podniebny", "pancerny"), 10},
    {("podniebny", "przebijajacy"), -10},

    {("uziemiajacy", "porazajacy"), -10},
    {("uziemiajacy", "specjalny"),-10},
    {("uziemiajacy", "podniebny"), 10},
    {("uziemiajacy", "przebijajacy"), -10},

    {("pancerny", "porazajacy"), -10},
    {("pancerny", "specjalny"),-10},
    {("pancerny", "podniebny"), 10},
    {("pancerny", "przebijajacy"), -10},

    {("przebijajacy", "specjalny"), -10},
    {("przebijajacy", "podniebny"),-10},
    {("przebijajacy", "uziemiajacy"), 10},
    {("przebijajacy", "pancerny"), -10},
  };
}