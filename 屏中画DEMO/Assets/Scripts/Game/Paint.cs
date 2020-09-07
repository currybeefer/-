using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Paint : MonoBehaviour
{
    private Transform m_Transform;//屏风本身的Transform组件
    public Transform groundTransform;//实际的地图的Transform组件
    public Transform cameraTransform;//每一块地图的摄像机的Transform组件
    private GameObject player;
    private Vector3 originPosition;//保存在移动位置之前时的位置
    private bool isClicked = false;
    private bool playerIn = false;
    private Animation m_Animation;
    public int state;
    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_Animation = gameObject.GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag("Player");
        state = 1;
    }

    void Update()
    {
        if (isClicked)//旋转的方法
        {
            if (state == 1)
            {
                isClicked = false;
                m_Animation.clip = m_Animation.GetClip("Rotate1");
                m_Animation.Play();
                Invoke("RotateGround", 0.5f);
                state = 2;
            }
        }
        if (isClicked)
        {
            if (state == 2)
            {
                isClicked = false;
                m_Animation.clip = m_Animation.GetClip("Rotate2");
                m_Animation.Play();
                Invoke("RotateGround", 0.5f);
                state = 1;
            }
        }
        if(player.GetComponent<Player>().gTag==groundTransform.tag)
        {
            playerIn = true;
        }
        else
        {
            playerIn = false;
        }
    }
    /// <summary>
    /// 旋转方法
    /// </summary>
    void RotateGround()
    {
        groundTransform.eulerAngles += new Vector3(0, 180, 0);
        if(playerIn)
        {
            player.transform.RotateAround(groundTransform.position,Vector3.up, 180);
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)&& !EventSystem.current.IsPointerOverGameObject())//如果玩家点击鼠标右键就会旋转
        {
            isClicked = true;
        }
    }
    IEnumerator OnMouseDown()
    {
        //!EventSystem.current.IsPointerOverGameObject如果当前鼠标在 ui 上返回true 否则返回false
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            originPosition = m_Transform.position;
            Vector3 temp = m_Transform.position;
            temp.z -= 1.5f;
            m_Transform.position = temp;//将屏风往前移动一点
            Vector3 screenSpace = Camera.main.WorldToScreenPoint(m_Transform.position);//三维物体坐标转屏幕坐标
                                                                                       //将鼠标屏幕坐标转为三维坐标，再计算物体位置与鼠标之间的距离
            Vector3 offset = m_Transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, screenSpace.z));

            while (Input.GetMouseButton(0))
            {
                Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, 0, screenSpace.z);
                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
                m_Transform.position = curPosition;
                yield return new WaitForFixedUpdate();
            }
        }
           
    }
    private void OnMouseUp()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            Replace();//放下时发出射线，互换地图块位置
        }
    }
    /// <summary>
    /// 屏风位置互换方法
    /// </summary>
    private void Replace()
    {
        Ray ray = new Ray(m_Transform.position, Vector3.forward);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit);
        if (isCollider)
        {
            if(hit.collider.gameObject.tag=="Screen")
            {
                //互换屏风位置
                Vector3 temp = hit.collider.gameObject.transform.position;
                hit.collider.gameObject.transform.position = originPosition;
                m_Transform.position = temp;
                //互换玩家的位置
                if (playerIn == true)//如果玩家在鼠标拖着的那个屏风上 
                {
                    //玩家的位置坐标随着屏风的移动而变换到移动后的位置
                    Vector3 shift = hit.collider.gameObject.GetComponent<Paint>().groundTransform.position - groundTransform.position;
                    player.transform.position += shift;
                }
                if (hit.collider.gameObject.GetComponent<Paint>().playerIn == true)//如果玩家在要被替换的屏风上
                {
                    Vector3 shift = hit.collider.gameObject.GetComponent<Paint>().groundTransform.position - groundTransform.position;
                    player.transform.position -= shift;
                }
                //互换地图块位置
                Vector3 gTemp = hit.collider.gameObject.GetComponent<Paint>().groundTransform.position;
                hit.collider.gameObject.GetComponent<Paint>().groundTransform.position = groundTransform.position;
                groundTransform.position = gTemp;
                //互换摄像机位置
                Vector3 cTemp = hit.collider.gameObject.GetComponent<Paint>().cameraTransform.position;
                hit.collider.gameObject.GetComponent<Paint>().cameraTransform.position = cameraTransform.position;
                cameraTransform.position = cTemp;
            }
        }
        else
        {
            m_Transform.position = originPosition;
        }
    }
}
