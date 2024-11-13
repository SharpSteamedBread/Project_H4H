using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSkipper : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            SceneManager.LoadScene("Map_Midboss");
        }
    }
}
