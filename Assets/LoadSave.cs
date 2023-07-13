using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSave : MonoBehaviour
{
    [SerializeField]
    private SaveManager saveManager;
    void Awake()
    {
        saveManager.LoadGame();
    }
}
