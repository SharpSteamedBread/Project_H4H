using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEnterUI : MonoBehaviour
{
    public GameObject objCommandEnterUI;

    public bool isCommandUIOpen = false;

    public Animator animPlayer;

    public float checkCommandOpenTime;

    private void Awake()
    {
    }

   
    // Update is called once per frame
    void Update()
    {
        if(checkCommandOpenTime >= 4.0f)
        {
            isCommandUIOpen = false;
        }

        if (isCommandUIOpen == false)
        {
            Time.timeScale = 1f;
            CommandCheckDict.isCommandSystemOpened = false;
            objCommandEnterUI.SetActive(false);

            StopCoroutine(CommandRestrict());
            checkCommandOpenTime = 0;
        }
    }

    public void InitPlayerAnim()
    {
        animPlayer.SetBool("useSkill1_1", false);
        animPlayer.SetBool("useSkill1_2", false);

        animPlayer.SetBool("useSkill2_1", false);
        animPlayer.SetBool("useSkill2_2", false);
        animPlayer.SetBool("useSkill2_3", false);

        animPlayer.SetBool("useSkill3_1", false);
        animPlayer.SetBool("useSkill3_2", false);
        animPlayer.SetBool("useSkill3_3", false);

        animPlayer.SetBool("useSkill4_1", false);
        animPlayer.SetBool("useSkill4_2", false);
    }

    public void TimeToSkillCommand()
    {
        Debug.Log("ÄÑÁø´Ù!");

        isCommandUIOpen = true;
        CommandCheckDict.isCommandSystemOpened = true;

        objCommandEnterUI.SetActive(isCommandUIOpen);
        InitPlayerAnim();
        StartCoroutine(CommandRestrict());
    }


    private IEnumerator CommandRestrict()
    {
        yield return new WaitForSecondsRealtime (1.0f);

        checkCommandOpenTime += 1.0f;

        StartCoroutine(CommandRestrict());

    }

}
