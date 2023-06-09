using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the inventory of ingredients and recipes
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public InventoryData InventoryData;

    public List<IngredientHolder> Inventory
    {
        get
        {
            return InventoryData.Inventory;
        }
        set
        {
            InventoryData.Inventory = value;
        }
    }

    public List<Recipe> Recipes
    {
        get
        {
            return InventoryData.Recipes;
        }
        set
        {
            InventoryData.Recipes = value;
        }
    }    

    // Start is called before the first frame update
    void Start()
    {
        InventoryData = new InventoryData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Adds an ingredient to the inventory
    /// </summary>
    /// <param name="ingredient"></param>
    public void AddIngredientToInventory(string ingredientName)
    {
        // Check if ingredient is already in inventory
        IngredientHolder ingredientHolder = Inventory.Find(i => i.IngredientName == ingredientName);

        if (ingredientHolder != null)
        {
            // If yes, increment quantity
            ingredientHolder.Quantity++;
        }
        else
        {
            // If no, add ingredient to inventory
            Inventory.Add(new IngredientHolder(ingredientName, 1));
        }
    }

    /// <summary>
    /// Removes an ingredient from the inventory
    /// </summary>
    /// <param name="ingredient"></param>
    public void RemoveIngredientFromInventory(string ingredientName)
    {
        // Check if ingredient is already in inventory
        IngredientHolder ingredientHolder = Inventory.Find(i => i.IngredientName == ingredientName);

        if (ingredientHolder != null)
        {
            // Check if quantity is greater than 1
            if (ingredientHolder.Quantity >= 1)
            {
                // If yes, decrement quantity
                ingredientHolder.Quantity--;

                // If ingredient quantity is zero, remove it from inventory
                if (ingredientHolder.Quantity == 0)
                {
                    Inventory.Remove(ingredientHolder);
                }
            }
            else
            {
                // Remove ingredient from inventory (should not be there)
                Inventory.Remove(ingredientHolder);

                Debug.LogError("Trying to remove an ingredient that is not in the inventory (quantity is 0)");
            }
        }
        else
        {
            Debug.LogError("Trying to remove an ingredient that is not in the inventory");
        }
    }

    /// <summary>
    /// Returns the quantity of a given ingredient
    /// </summary>
    /// <param name="ingredientName"></param>
    /// <returns>Quantity of given ingredient</returns>
    public int GetIngredientQuantity(string ingredientName)
    {
        IngredientHolder ingredient = Inventory.Find(Inventory => Inventory.IngredientName == ingredientName);

        if (ingredient != null)
            return ingredient.Quantity;

        return 0;
    }

    /// <summary>
    /// Adds a recipe to the list of recipes
    /// </summary>
    /// <param name="recipe"></param>
    public void AddRecipeToInventory(Recipe recipe)
    {
        Recipes.Add(recipe);
    }

    /// <summary>
    /// Removes a recipe from the list of recipes
    /// </summary>
    /// <param name="recipe"></param>
    public void RemoveRecipeFromInventory(Recipe recipe)
    {
        Recipes.Remove(recipe);
    }
}
