using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小精灵提示面板
/// </summary>
public class ScorePanel : MonoBehaviour
{
    public void Drag()
    {
       transform.localPosition =new Vector2(transform.localPosition.x, Input.mousePosition.y - Screen.height / 2);
    }
    public void DragEnd()
    {
        if(transform.position.y>=156)
        {
            transform.position = new Vector2(transform.position.x, 290);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, 22);
        }
    }
    private void Update()
    {
        if (transform.position.y > 290)
        {
            transform.position = new Vector2(transform.position.x, 290);
        }
        if(transform.position.y<22)
        {
            transform.position = new Vector2(transform.position.x, 22);
        }
    }

}
