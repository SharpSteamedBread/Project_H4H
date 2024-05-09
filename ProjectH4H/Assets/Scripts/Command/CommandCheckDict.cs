using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandCheckDict : MonoBehaviour
{
    private Dictionary<string, System.Action> commandDictionary = new Dictionary<string, System.Action>();

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

    public bool isCommandUIOpen = false;

    static public bool isCommandSystemOpened = false;

    private void Awake()
    {
        // 커맨드와 처리할 메소드 매핑
        commandDictionary.Add("A", ActivateSkill1_1);
        commandDictionary.Add("AEQ", ActivateSkill1_2);

        commandDictionary.Add("EE", ActivateSkill2_1);
        commandDictionary.Add("EEQQ", ActivateSkill2_2);
        commandDictionary.Add("EEQQE", ActivateSkill2_3);

        commandDictionary.Add("DA", ActivateSkill3_1);
        commandDictionary.Add("DAQE", ActivateSkill3_2);
        commandDictionary.Add("DAQEEQ", ActivateSkill3_3);

        commandDictionary.Add("DD", ActivateSkill4_1);
        commandDictionary.Add("DDA", ActivateSkill4_2);
    }

    void Update()
    {
        ToggleCommandEnterUI();
        ProcessCommand(inputCommand);

        if (objCommandEnterUI.activeSelf == true)
        {
            Time.timeScale = bulletTime;
            isCommandSystemOpened = true;

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
                InitAnimation();

                isCommandUIOpen = !isCommandUIOpen;
                isCommandSystemOpened = false;

            }
        }
    }

    // 스킬 처리 메소드
    private void ProcessCommand(string command)
    {
        if (commandDictionary.ContainsKey(command))
        {
            commandDictionary[command].Invoke();
        }
        else
        {
            //Debug.LogWarning("Unknown command: " + command);
        }
    }

    private void ToggleCommandEnterUI()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isCommandUIOpen = !isCommandUIOpen;
            objCommandEnterUI.SetActive(isCommandUIOpen!);
        }

        if (isCommandUIOpen == false)
        {
            Time.timeScale = 1f;
        }
    }

    private void ActivateSkill1_1()
    {
        animPlayer.SetBool("useSkill1_1", true);
    }

    private void ActivateSkill1_2()
    {
        animPlayer.SetBool("useSkill1_2", true);

        ExitSkill();
    }

    private void ActivateSkill2_1()
    {
        animPlayer.SetBool("useSkill2_1", true);
    }

    private void ActivateSkill2_2()
    {
        animPlayer.SetBool("useSkill2_2", true);
    }

    private void ActivateSkill2_3()
    {
        animPlayer.SetBool("useSkill2_3", true);

        ExitSkill();
    }

    private void ActivateSkill3_1()
    {
        animPlayer.SetBool("useSkill3_1", true);
    }

    private void ActivateSkill3_2()
    {
        animPlayer.SetBool("useSkill3_2", true);
    }

    private void ActivateSkill3_3()
    {
        animPlayer.SetBool("useSkill3_3", true);

        ExitSkill();
    }

    private void ActivateSkill4_1()
    {
        animPlayer.SetBool("useSkill4_1", true);
    }

    private void ActivateSkill4_2()
    {
        animPlayer.SetBool("useSkill4_2", true);
        ExitSkill();
    }


    //스킬 시전 후 초기화
    private void ExitSkill()
    {

        foreach (Transform child in assortObjCommandKey.transform)
        {
            Destroy(child.gameObject);
        }

        isCommandUIOpen = false;
        objCommandEnterUI.SetActive(false);
        Time.timeScale = 1.0f;
        isCommandSystemOpened = false;

        InitAnimation();
    }

    private void InitAnimation()
    {
        inputCommand = "";


        animPlayer.SetBool("useSkill1_1", false);

        animPlayer.SetBool("useSkill2_1", false);
        animPlayer.SetBool("useSkill2_2", false);

        animPlayer.SetBool("useSkill3_1", false);
        animPlayer.SetBool("useSkill3_2", false);

        animPlayer.SetBool("useSkill4_1", false);
    }
}