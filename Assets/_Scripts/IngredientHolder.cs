using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains an ingredient and its quantity
/// </summary>
public class IngredientHolder : MonoBehaviour
{
    public Ingredient Ingredient;
    public int Quantity;
    public string IngredientName => Ingredient.Name;

    public IngredientHolder(Ingredient ingredient, int quantity)
    {
        Ingredient = ingredient;
        Quantity = quantity;
    }
}
