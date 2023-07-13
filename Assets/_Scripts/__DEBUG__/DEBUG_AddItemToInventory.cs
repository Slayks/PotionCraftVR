using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_AddItemToInventory : MonoBehaviour
{
    public GameObject Ingredient;
    public GameObject Recipe;
    public GameObject InventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient()
    {
        InventoryManager.GetComponent<InventoryManager>().AddIngredientToInventory(Ingredient.GetComponent<Ingredient>());
    }

    public void RemoveIngredient()
    {
        InventoryManager.GetComponent<InventoryManager>().RemoveIngredientFromInventory(Ingredient.GetComponent<Ingredient>());
    }

    public void AddRecipe()
    {
        InventoryManager.GetComponent<InventoryManager>().AddRecipeToInventory(Recipe.GetComponent<Recipe>());
    }

    public void RemoveRecipe()
    {
        InventoryManager.GetComponent<InventoryManager>().RemoveRecipeFromInventory(Recipe.GetComponent<Recipe>());
    }
}
