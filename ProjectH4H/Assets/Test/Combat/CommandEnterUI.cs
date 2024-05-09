using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEnterUI : MonoBehaviour
{
    public GameObject objCommandEnterUI;

    public bool isCommandUIOpen = false;

    public Animator animPlayer;

    private void Awake()
    {
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isCommandUIOpen = !isCommandUIOpen;
            objCommandEnterUI.SetActive(isCommandUIOpen!);
            InitPlayerAnim();
        }

        if (isCommandUIOpen == false)
        {
            Time.timeScale = 1f;
            CommandCheck.isCommandSystemOpened = false;
            objCommandEnterUI.SetActive(false);
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


}
