using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageResultCounter : MonoBehaviour
{
    public static int enemyKillCount = 0;

    public int stageTimeattack = 0;
    public static int stageTimeMinute = 0;
    public static int stageTimeSecond = 0;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(TimeCounter());
    }

    // Update is called once per frame
    void Update()
    {
        stageTimeMinute = stageTimeattack / 60;
        stageTimeSecond = stageTimeattack % 60;

        //Debug.Log($"{stageTimeMinute}분 {stageTimeSecond}초 경과!");
        
    }

    public IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(1f);

        stageTimeattack++;

        StartCoroutine(TimeCounter());
    }
}
