using System.Collections.Generic;

public static class TagBonuses
{
    public static readonly Dictionary<(string attack, string target), int> bonusDMG = new Dictionary<(string attack, string target), int>(){
    {("fire","water"), 5},
    {("fire","plant"),-5},
    {("water", "fire"), 5},
    {("water", "plant"), -5},
    {("plant", "fire"), -5},
    {("plant", "water"), 5}
  };
}