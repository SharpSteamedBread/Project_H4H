using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

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
    [SerializeField] private Canvas canvasDialog;

    [Header("애니메이션")]
    [SerializeField] private GameObject anim1_enemy4Born;
    [SerializeField] private float anim09Delay = 2f;

    [Space(10)]
    [SerializeField] private GameObject anim2_enemy4wakeup;
    [SerializeField] private Animator anim2_enemyAnim;

    [Space(10)]
    [SerializeField] private GameObject anim3_portal;


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
            StartCoroutine(TypingText());

        }

        if (number > 30)
        {
            SceneManager.LoadScene("Map_Midboss");
        }

        DialogController();
    }

    private void NextDialog()
    {
        if(number != 18)
        {
            //dialogText = dialogList01.dialogMidboss[number].charDialog;
        }
        number++;
    }

    private IEnumerator TypingText()
    {
        for(int i = 0; i < dialogList01.dialogMidboss[number - 1].charDialog.Length; i++)
        {
            dialogText = dialogList01.dialogMidboss[number - 1].charDialog.Substring(0, i + 1);

            yield return new WaitForSeconds(0.03f);

        }
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
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);

                break;

            case 3:
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 4:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 5:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 6:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 7:
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 8:
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 9:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 10:
                anim1_enemy4Born.SetActive(true);

                StartCoroutine(ChatDelay09());
                break;

            case 11:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 12:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);

                anim1_enemy4Born.SetActive(false);
                anim2_enemy4wakeup.SetActive(true);
                anim2_enemyAnim.SetInteger("animNumber", 2);

                break;

            case 13:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
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
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 17:
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 18:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 19:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 20:
                uiOs.SetActive(true);
                textOs.text = dialogText;
                uiMos.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 21:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 22:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 23:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 24:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 25:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 26:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 27:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 28:
                uiMos.SetActive(true);
                textMos.text = dialogText;
                uiOs.SetActive(false);
                uiShin.SetActive(false);
                break;

            case 29:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;

            case 30:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);

                anim3_portal.SetActive(true);
                break;

            case 31:
                uiShin.SetActive(true);
                textShin.text = dialogText;
                uiOs.SetActive(false);
                uiMos.SetActive(false);
                break;
        }
    }

    private IEnumerator ChatDelay09()
    {
        yield return new WaitForSeconds(anim09Delay);

        uiMos.SetActive(true);
        textMos.text = dialogText;
        uiOs.SetActive(false);
        uiShin.SetActive(false);
    }
}
