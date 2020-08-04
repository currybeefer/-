using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform m_Transform;
    public string gTag;
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        gTag = collision.transform.tag;
    }
}
