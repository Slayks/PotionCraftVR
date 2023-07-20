using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void LoadLevel()
    {
        SceneManager.LoadScene("MoveInFog");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("collisioon");
            LoadLevel();
        }
        else
        {
            Debug.Log("test");
        }
    }
}
