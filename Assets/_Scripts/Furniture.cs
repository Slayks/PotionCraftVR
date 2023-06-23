using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Furniture : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> path;

    [SerializeField]
    private GameObject prefab;
    private GameObject instantiatedPrefab;

    public delegate void FurnitureGrabbed(Furniture furniture);
    public static event FurnitureGrabbed OnFurnitureGrabbed;
    public delegate void FurnitureDropped();
    public static event FurnitureDropped OnFurnitureDropped;


    private void Start()
    {
        this.instantiatedPrefab = Instantiate(prefab, this.transform);
    }

    // TODO à remplacer par une branchement sur l'event de grab ou de ungrab
    public void OnMouseDown()
    {
        Furniture.OnFurnitureDropped.Invoke();
        Furniture.OnFurnitureGrabbed.Invoke(this);
    }

    public List<Vector3> GetPath()
    {
        return path;
    }
}
