using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageResultCounter : MonoBehaviour
{
    public static int enemyKillCount = 0;
    public static int stageTimeattack = 0;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(TimeCounter());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(1f);

        stageTimeattack++;

        StartCoroutine(TimeCounter());
    }
}
