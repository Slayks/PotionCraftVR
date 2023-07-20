using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class DistributeIngredient : XRBaseInteractable
{
    /// <summary>
    /// A prefab of the entire ingredient
    /// </summary>
    [SerializeField]
    private Ingredient ingredientPrefab;
    /// <summary>
    /// The visual prefab of ingredient that will be distributed by this game object based on the ingredientPrefab variable
    /// </summary>
    private GameObject instantiatedIngredientPrefab;

    [SerializeField]
    private InventoryManager inventoryManager;

    private TMP_Text textMeshPro;

    private void Start()
    {
        this.instantiatedIngredientPrefab = Instantiate(ingredientPrefab.GetPrefab(), this.transform);
        Vector3 localScale = this.instantiatedIngredientPrefab.transform.localScale;
        Vector3 localPosition = this.instantiatedIngredientPrefab.transform.localPosition;
        float newScale = localScale.x * 3;
        this.instantiatedIngredientPrefab.transform.localScale = new Vector3(newScale, newScale, newScale);
        localPosition.y = -.2f;
        this.instantiatedIngredientPrefab.transform.localPosition = localPosition;

        textMeshPro = this.gameObject.transform.parent.GetComponentInChildren<TMP_Text>();
        int ingredientQuantity = this.inventoryManager.GetIngredientQuantity(this.ingredientPrefab.Name);
        textMeshPro.text = ingredientQuantity.ToString();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        CreateAndSelectIngredient(args);
    }

    private void CreateAndSelectIngredient(SelectEnterEventArgs args)
    {
        Ingredient ingredient = CreateIngredient(args.interactorObject.transform);
        interactionManager.SelectEnter(args.interactorObject, ingredient);
        inventoryManager.RemoveIngredientFromInventory(ingredient.Name);
        textMeshPro.text = this.inventoryManager.GetIngredientQuantity(ingredient.Name).ToString();
    }

    private Ingredient CreateIngredient(Transform transform)
    {
        GameObject ingredientObject = Instantiate(this.ingredientPrefab.gameObject, transform.position, transform.rotation);
        return ingredientObject.GetComponent<Ingredient>();
    }
}
