using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeUIButton : MonoBehaviour
{


    public void RestartScene()
    {
        SceneManager.LoadScene("SpineTest");
    }
}
