using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
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

    private void Start()
    {
        this.instantiatedIngredientPrefab = Instantiate(ingredientPrefab.GetPrefab(), this.transform);
        Vector3 localScale = this.instantiatedIngredientPrefab.transform.localScale;
        Vector3 localPosition = this.instantiatedIngredientPrefab.transform.localPosition;
        float newScale = localScale.x * 3;
        this.instantiatedIngredientPrefab.transform.localScale = new Vector3(newScale, newScale, newScale);
        localPosition.y = -.2f;
        this.instantiatedIngredientPrefab.transform.localPosition = localPosition;
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
    }

    private Ingredient CreateIngredient(Transform transform)
    {
        GameObject ingredientObject = Instantiate(this.ingredientPrefab.gameObject, transform.position, transform.rotation);
        return ingredientObject.GetComponent<Ingredient>();
    }
}
