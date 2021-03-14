using System.Collections.Generic;

public static class TagBonuses
{
    public static readonly Dictionary<(string attack, string target), int> bonusDMG = new Dictionary<(string attack, string target), int>(){
    {("porazajacy", "specjalny"), 5},
    {("porazajacy", "podniebny"),-5},
    {("porazajacy", "uziemiajacy"), 5},
    {("porazajacy", "pancerny"), -5},

    {("specjalny", "porazajacy"), -5},
    {("specjalny", "uziemiajacy"),-5},
    {("specjalny", "pancerny"), 5},
    {("specjalny", "przebijajacy"), -5},

    {("podniebny", "porazajacy"), -5},
    {("podniebny", "uziemiajacy"),-5},
    {("podniebny", "pancerny"), 5},
    {("podniebny", "przebijajacy"), -5},

    {("uziemiajacy", "porazajacy"), -5},
    {("uziemiajacy", "specjalny"),-5},
    {("uziemiajacy", "podniebny"), 5},
    {("uziemiajacy", "przebijajacy"), -5},

    {("pancerny", "porazajacy"), -5},
    {("pancerny", "specjalny"),-5},
    {("pancerny", "podniebny"), 5},
    {("pancerny", "przebijajacy"), -5},

    {("przebijajacy", "specjalny"), -5},
    {("przebijajacy", "podniebny"),-5},
    {("przebijajacy", "uziemiajacy"), 5},
    {("przebijajacy", "pancerny"), -5},
  };
}