using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeUIButton : MonoBehaviour
{
    public GameObject objEnemy4;

    public void Awake()
    {
        objEnemy4.GetComponent<EnemyFourthStatus>();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("SpineTest");
    }

    public void KillEnemy4()
    {
        objEnemy4.GetComponent<EnemyFourthStatus>().EnemyDie();
    }
}
