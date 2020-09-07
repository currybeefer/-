using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SwitchScene : MonoBehaviour
{ 
    public void Switch()
    {
        SceneManager.LoadScene(1);
    }
}
