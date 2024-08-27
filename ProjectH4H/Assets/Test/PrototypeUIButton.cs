using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeUIButton : MonoBehaviour
{
    

    public void Awake()
    {
        //objEnemy4.GetComponent<EnemyFourthStatus>();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

   
}
