using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text progressText;
    private int progress=0;
    private void Start()
    {
        progressText.text = "0/4";
    }
    void AddProgress()
    {
        progress += 1;
        progressText.text = progress + "/4";
    }
    
}
