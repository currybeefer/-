using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform m_Transform;
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
    }
    
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        m_Transform.SetParent(collision.transform);
    }
}
