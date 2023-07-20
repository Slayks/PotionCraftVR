using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            LoadLevel();
        }
    }
}
