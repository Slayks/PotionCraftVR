using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Ingredient : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> path;

    [SerializeField]
    private GameObject prefab;
    private GameObject instantiatedPrefab;

    public delegate void IngredientGrabbed(Ingredient ingredient);
    public static event IngredientGrabbed OnIngredientGrabbed;
    public delegate void IngredientDropped();
    public static event IngredientDropped OnIngredientDropped;


    private void Start()
    {
        this.instantiatedPrefab = Instantiate(prefab, this.transform);
    }

    // TODO à remplacer par une branchement sur l'event de grab ou de ungrab
    public void OnMouseDown()
    {
        Ingredient.OnIngredientDropped.Invoke();
        Ingredient.OnIngredientGrabbed.Invoke(this);
    }

    public List<Vector3> GetPath()
    {
        return path;
    }
}
