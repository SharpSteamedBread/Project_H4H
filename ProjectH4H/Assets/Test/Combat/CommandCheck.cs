using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandCheck : MonoBehaviour
{
    public Animator animPlayer;
    public float bulletTime = 0.1f;

    [Header("UI 키버튼")]
    public GameObject objCommandKeyQ;
    public GameObject objCommandKeyE;
    public GameObject objCommandKeyA;
    public GameObject objCommandKeyD;
    public GameObject assortObjCommandKey;

    public string inputCommand;


    [Header("커맨드 UI")]

    public GameObject objCommandEnterUI;

    static public bool isCommandSystemOpened = false;
    //public bool checkCommandSystemOpened = false;


    // Update is called once per frame
    void Update()
    {
        //checkCommandSystemOpened = isCommandSystemOpened;

        if (objCommandEnterUI.activeSelf == true)
        {
            Time.timeScale = bulletTime;
            isCommandSystemOpened = true;

            CorrectCommand();

            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject cloneQ = Instantiate(objCommandKeyQ, objCommandKeyQ.transform.position, objCommandKeyQ.transform.rotation);
                cloneQ.transform.SetParent(assortObjCommandKey.transform);
                cloneQ.transform.localScale = Vector3.one * 0.7f;
                inputCommand = inputCommand + "Q";
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject cloneE = Instantiate(objCommandKeyE, objCommandKeyE.transform.position, objCommandKeyE.transform.rotation);
                cloneE.transform.SetParent(assortObjCommandKey.transform);
                cloneE.transform.localScale = Vector3.one * 0.7f;
                inputCommand = inputCommand + "E";
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                GameObject cloneA = Instantiate(objCommandKeyA, objCommandKeyA.transform.position, objCommandKeyA.transform.rotation);
                cloneA.transform.SetParent(assortObjCommandKey.transform);
                cloneA.transform.localScale = Vector3.one * 0.7f;
                inputCommand = inputCommand + "A";
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                GameObject cloneD = Instantiate(objCommandKeyD, objCommandKeyD.transform.position, objCommandKeyD.transform.rotation);
                cloneD.transform.SetParent(assortObjCommandKey.transform);
                cloneD.transform.localScale = Vector3.one * 0.7f;
                inputCommand = inputCommand + "D";
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inputCommand = "";

                foreach (Transform child in assortObjCommandKey.transform)
                {
                    Destroy(child.gameObject);
                }

                animPlayer.SetTrigger("commandCancelled");
                animPlayer.SetBool("useSkill1_1 0", false);
                animPlayer.SetBool("useSkill1_2 0", false);

                isCommandSystemOpened = false;
                gameObject.GetComponent<CommandEnterUI>().isCommandUIOpen = gameObject.GetComponent<CommandEnterUI>().isCommandUIOpen!;

            }
        }
    }

    private void CorrectCommand()
    {

        if (inputCommand == ("A"))
        {
            animPlayer.SetTrigger("useSkill1_1");
            animPlayer.SetBool("useSkill1_1 0", true);
        }

        if (inputCommand.Contains("AEQ"))
        {
            animPlayer.SetTrigger("useSkill1_2");
            animPlayer.SetBool("useSkill1_2 0", true);

            inputCommand = "";


            foreach (Transform child in assortObjCommandKey.transform)
            {
                Destroy(child.gameObject);
            }

            isCommandSystemOpened = false;
            gameObject.GetComponent<CommandEnterUI>().isCommandUIOpen = false;

            animPlayer.SetBool("useSkill1_1", false);

        }
    }
}