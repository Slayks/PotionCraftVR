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
        Furniture.OnFurnitureGrabbed += DisplayPath;
        Furniture.OnFurnitureDropped += RemoveDisplayedPath;
    }

    private void OnDestroy()
    {
        Furniture.OnFurnitureGrabbed -= DisplayPath;
        Furniture.OnFurnitureDropped -= RemoveDisplayedPath;
    }

    public void DisplayPath(Furniture furniture)
    {
        this.platform.DisplayPath(furniture.GetPath());
    }

    public void RemoveDisplayedPath()
    {
        platform.RemoveDisplayedPath();
    }
}
