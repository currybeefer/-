using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spirit : MonoBehaviour
{
    public Image bar;//获取进度条
    bool couldPick = false;
    private float delay = 0.5f;
    public bool isDown = false;
    private float lastIsDownTime;
    void Start()
    {
    }
    
    void Update()
    {
        if(couldPick)
        {
            if (isDown)//检测鼠标左键的长按
            {
                if (Time.time - lastIsDownTime > delay)
                {
                    FillBar();
                }
            }
            if (isDown == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isDown = true;
                    lastIsDownTime = Time.time;
                }
                UnFillBar();//如果鼠标没有长按，进度条将减少到0
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDown = false;
            }
        }
    }
    /// <summary>
    /// 填充进度条事件
    /// </summary>
    void FillBar()
    {
        if(bar.fillAmount>=1)
        {
            couldPick = false;
            PickUpCompelete();
        }
        bar.fillAmount += 0.02f;
    }
    /// <summary>
    /// 减少进度条事件
    /// </summary>
    void UnFillBar()
    {
        if(bar.fillAmount<=0)
        {
            bar.fillAmount = 0;
        }
        else
        {
            bar.fillAmount -= 0.02f;
        }
    }
    /// <summary>
    /// 完成捡起小精灵事件
    /// </summary>
    void PickUpCompelete()
    {
        GameObject.Find("GameManager").SendMessage("AddProgress");
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            couldPick = true;
        }
    }
}
