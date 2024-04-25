using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandCheck : MonoBehaviour
{
    public float bulletTime = 0.1f;

    public GameObject objCommandKeyQ;
    public GameObject objCommandKeyE;
    public GameObject objCommandKeyA;
    public GameObject objCommandKeyD;

    public GameObject assortObjCommandKey;

    public GameObject objCommandEnterUI;

    public string inputCommand;

    public Animator animPlayer;

    private bool attack1_1Triggered = false;

    static public bool isCommandSystemOpened = false;


    // Update is called once per frame
    void Update()
    {
        if(objCommandEnterUI.activeSelf == true)
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

                animPlayer.SetBool("commandCancelled", true);

                gameObject.GetComponent<CommandEnterUI>().isCommandUIOpen = gameObject.GetComponent<CommandEnterUI>().isCommandUIOpen!;
                isCommandSystemOpened = false;
                animPlayer.SetBool("commandCancelled", false);

            }
        }
    }

    private void CorrectCommand()
    {

        if (inputCommand == ("A") && !attack1_1Triggered)
        {
            animPlayer.SetTrigger("doAttack1_1");
            attack1_1Triggered = true;
        }

        if (inputCommand.Contains("EQ"))
        {
            animPlayer.SetTrigger("doAttack1_2");

            inputCommand = "";
            attack1_1Triggered = false;


            foreach (Transform child in assortObjCommandKey.transform)
            {
                Destroy(child.gameObject);
            }

            gameObject.GetComponent<CommandEnterUI>().isCommandUIOpen = false;


            objCommandEnterUI.SetActive(false);
            Time.timeScale = 1.0f;
            isCommandSystemOpened = false;

        }
    }
}