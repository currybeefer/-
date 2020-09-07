using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform m_Transform;
    public string gTag;//gTag用于获取player所站在的地块的标签
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag!="Untagged")
        {
            gTag = collision.transform.tag;//当前player位于哪个地块上，gTag的值就是哪个
        }
    }
}
