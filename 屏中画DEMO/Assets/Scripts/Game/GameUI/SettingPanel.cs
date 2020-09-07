using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingPanel : MonoBehaviour
{
    public void SwitchToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
