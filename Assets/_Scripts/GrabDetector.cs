using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class GrabDetector : MonoBehaviour
{
    [SerializeField]
    private Platform platform;

    void Start()
    {
        Ingredient.OnIngredientGrabbed += DisplayPath;
        Ingredient.OnIngredientDropped += RemoveDisplayedPath;
    }

    private void OnDestroy()
    {
        Ingredient.OnIngredientGrabbed -= DisplayPath;
        Ingredient.OnIngredientDropped -= RemoveDisplayedPath;
    }

    public void DisplayPath(Ingredient ingredient)
    {
        this.platform.DisplayPath(ingredient.GetPath());
    }

    public void RemoveDisplayedPath()
    {
        platform.RemoveDisplayedPath();
    }
}
