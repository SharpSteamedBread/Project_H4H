using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSkipper : MonoBehaviour
{
    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Map_Tutorial_2" &&
            Input.GetKeyUp(KeyCode.Tab))
        {
            SceneManager.LoadScene("Map_Stage");
        }

        else if (SceneManager.GetActiveScene().name == "Map_DialogBeforeBoss" &&
            Input.GetKeyUp(KeyCode.Tab))
        {
            SceneManager.LoadScene("Map_Midboss");
        }
    }
}
