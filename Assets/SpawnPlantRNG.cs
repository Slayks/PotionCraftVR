using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlantRNG : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> plant1;
    [SerializeField]
    private List<GameObject> plant2;
    [SerializeField]
    private List<GameObject> plant3;
    [SerializeField]
    private List<GameObject> plant4;
    [SerializeField]
    private List<GameObject> plant5;
    [SerializeField]
    private List<GameObject> plant6;
    [SerializeField]
    private List<GameObject> plant7;
    [SerializeField]
    private List<GameObject> plant8;

    // Start is called before the first frame update
    void Start()
    {
        Spawn(plant1);
        Spawn(plant2);
        Spawn(plant3);
        Spawn(plant4);
        Spawn(plant5);
        Spawn(plant6);
        Spawn(plant7);
        Spawn(plant8);
    }

    void Spawn(List<GameObject> listObjects)
    {
        for (int i = 0; i < listObjects.Count; i++)
        {
            listObjects[i].SetActive(false);
            if (i % 3 == 0)
            {
                listObjects[i].SetActive(true);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
