using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class storing the recipes by storing the ingredients used and their quantities
/// </summary>
public class Recipe
{
    public string Name;
    public List<IngredientHolder> Ingredients = new List<IngredientHolder>();
}
