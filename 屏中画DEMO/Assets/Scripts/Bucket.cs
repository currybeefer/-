using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    public GameObject spirit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="WaterSpirit")
        {
            other.gameObject.SetActive(false);
            spirit.SetActive(true);
        }
    }

}
