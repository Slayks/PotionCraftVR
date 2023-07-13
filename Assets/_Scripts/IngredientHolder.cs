using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains an ingredient and its quantity
/// </summary>
public class IngredientHolder
{
    public string IngredientName;
    public int Quantity;

    public IngredientHolder(string ingredientName, int quantity)
    {
        IngredientName = ingredientName;
        Quantity = quantity;
    }
}
