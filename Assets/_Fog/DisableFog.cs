using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR;

public class DisableFog : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.layer == 6)
        {
            this.gameObject.SetActive(false);
        }
    }
}
