using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    private Transform m_Transform;
    public float speed=100;
    private bool isClicked = false;
    public Animation m_Animation;
    public int state;
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animation = gameObject.GetComponent<Animation>();
        state = 1;
    }
    
    void Update()
    {
        if(isClicked)
        {
            if (state == 1)
            {
                isClicked = false;
                m_Animation.clip = m_Animation.GetClip("Rotate1");
                m_Animation.Play();
                state = 2;
            }
        }
        if(isClicked)
        {
            if (state == 2)
            {
                isClicked = false;
                m_Animation.clip = m_Animation.GetClip("Rotate2");
                m_Animation.Play();
                state = 1;
            }
        }
    }
    private void OnMouseDown()
    {
        isClicked = true;
    }

}
