using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEnterUI : MonoBehaviour
{
    public GameObject objCommandEnterUI;
    public bool isCommandUIOpen = false;
    public Animator animPlayer;

    public float checkCommandOpenTime;
    public float commandUIEnableTime = 3.0f;

    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isCommandUIOpen)
        {
            checkCommandOpenTime += Time.unscaledDeltaTime;

            if (checkCommandOpenTime >= commandUIEnableTime)
            {
                isCommandUIOpen = false;
                Time.timeScale = 1f;
                CommandCheckDict.isCommandSystemOpened = false;
                objCommandEnterUI.SetActive(false);
                checkCommandOpenTime = 0;
            }
        }
    }

    public void InitPlayerAnim()
    {
        animPlayer.SetBool("useSkill1_1", false);

        animPlayer.SetBool("useSkill2_1", false);
        animPlayer.SetBool("useSkill2_2", false);

        animPlayer.SetBool("useSkill3_1", false);
        animPlayer.SetBool("useSkill3_2", false);

        animPlayer.SetBool("useSkill4_1", false);

        animPlayer.SetBool("useSkill6_1", false);

        animPlayer.SetBool("ZAttackCombo1", false);
        animPlayer.SetBool("ZAttackCombo2", false);
        animPlayer.SetBool("ZAttackCombo3", false);

        animPlayer.SetBool("XAttackCombo1", false);
        animPlayer.SetBool("XAttackCombo2", false);
        animPlayer.SetBool("XAttackCombo3", false);

        animPlayer.SetBool("isMoving", false);
    }

    public void TimeToSkillCommand()
    {
        Debug.Log("ÄÑÁø´Ù!");

        isCommandUIOpen = true;
        CommandCheckDict.isCommandSystemOpened = true;

        objCommandEnterUI.SetActive(isCommandUIOpen);
        InitPlayerAnim();

        animPlayer.SetTrigger("commandCancelled");
        animPlayer.Play("player_idle", -1, 0f);

        checkCommandOpenTime = 0;  // Reset the timer
    }
}
