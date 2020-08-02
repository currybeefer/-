using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Transform m_Transform;
    public float speed = 100;
    private bool isClicked = false;
    private Animation m_Animation;
    public int state;
    private Vector3 originPosition;
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animation = gameObject.GetComponent<Animation>();
        state = 1;
    }

    void Update()
    {
        if (isClicked)//旋转的方法
        {
            if (state == 1)
            {
                isClicked = false;
                m_Animation.clip = m_Animation.GetClip("Rotate2");
                m_Animation.Play();
                state = 2;
            }
        }
        if (isClicked)
        {
            if (state == 2)
            {
                isClicked = false;
                m_Animation.clip = m_Animation.GetClip("Rotate1");
                m_Animation.Play();
                state = 1;
            }
        }
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))//如果玩家点击鼠标右键就会旋转
        {
            isClicked = true; 
        }
    }
    IEnumerator OnMouseDown()
    {
        originPosition = m_Transform.position;

        Vector3 temp = m_Transform.position;
        temp.y = 3;
        m_Transform.position = temp;
        Vector3 screenSpace = Camera.main.WorldToScreenPoint(m_Transform.position);//三维物体坐标转屏幕坐标
        //将鼠标屏幕坐标转为三维坐标，再计算物体位置与鼠标之间的距离
        Vector3 offset = m_Transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,0, screenSpace.z));
        while (Input.GetMouseButton(0))
        {
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, 0, screenSpace.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            m_Transform.position = curPosition;
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnMouseUp()
    {
        Replace();//放下时发出射线，互换地图块位置
    }
    private void Replace()
    {
        Ray ray = new Ray(m_Transform.position, Vector3.down);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray,out hit);
        if(isCollider)
        {
            if(hit.collider.gameObject.tag=="Ground")
            {
                Vector3 temp = hit.collider.gameObject.transform.position;
                hit.collider.gameObject.transform.position = originPosition;
                m_Transform.position = temp;
            }
        }
    }
}
