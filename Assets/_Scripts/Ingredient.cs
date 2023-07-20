using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Ingredient : XRGrabInteractable
{
    [SerializeField]
    private List<Vector3> path;

    [SerializeField]
    private GameObject prefab;
    private GameObject instantiatedPrefab;

    public string Name;

    public delegate void IngredientGrabbed(Ingredient ingredient);
    public static event IngredientGrabbed OnIngredientGrabbed;
    public delegate void IngredientDropped();
    public static event IngredientDropped OnIngredientDropped;


    protected override void Awake()
    {
        base.Awake();
        this.Name = prefab.name;
        this.instantiatedPrefab = Instantiate(prefab, this.transform);
        this.colliders.Add(this.instantiatedPrefab.GetComponent<Collider>());
        this.selectEntered.AddListener(EmitGrabbedEvent);
        this.selectExited.AddListener(EmitDroppedEvent);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        this.selectEntered.RemoveListener(EmitGrabbedEvent);
        this.selectExited.RemoveListener(EmitDroppedEvent);
    }

    public void EmitGrabbedEvent(SelectEnterEventArgs args)
    {
        Ingredient.OnIngredientDropped.Invoke();
        Ingredient.OnIngredientGrabbed.Invoke(this);
    }

    public void EmitDroppedEvent(SelectExitEventArgs args)
    {
        Ingredient.OnIngredientDropped.Invoke();
    }

    public List<Vector3> GetPath()
    {
        return path;
    }

    public GameObject GetPrefab()
    {
        return this.prefab;
    }
}
