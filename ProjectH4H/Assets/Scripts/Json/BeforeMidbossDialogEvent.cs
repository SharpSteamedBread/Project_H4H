using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BeforeMidbossDialogEvent : MonoBehaviour
{
    public DialogManager dialogManager;

    public DialogList dialogList01;             // JSON에서 불러온 Dialog 데이터 리스트

    /*
    json list 사용법
    dialogList01.dialogMidboss[charDialog]
    */
    [Header("변수")]
    [SerializeField] private int number = 0;
    [SerializeField] private string dialogText = "test";

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textOs;
    [SerializeField] private TextMeshProUGUI textMos;
    [SerializeField] private TextMeshProUGUI textShin;

    [Space(10)]
    [SerializeField] private GameObject uiOs;
    [SerializeField] private GameObject uiMos;
    [SerializeField] private GameObject uiShin;

    private void Awake()
    {
        number = 0;
    }

    void Start()
    {
        dialogList01 = dialogManager.dialogList01;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NextDialog();
        }

        if (number > 18)
        {
            SceneManager.LoadScene("Map_Midboss");
        }

        DialogController();
    }

    private void NextDialog()
    {
        if(number != 18)
        {
            dialogText = dialogList01.dialogMidboss[number].charDialog;
        }
        number++;
    }

    private void DialogController()
    {
        switch(number)
        {
            case 1:
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 2:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);

                break;

            case 3:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 4:
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 5:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 6:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 7:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 8:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 9:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 10:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 11:
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 12:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 13:
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 14:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 15:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 16:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 17:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 18:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;
        }
    }
}
