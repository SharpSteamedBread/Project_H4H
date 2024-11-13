using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class ResultUIController : MonoBehaviour
{
    [SerializeField] private Button btnStart;
    [SerializeField] private TextMeshProUGUI txtEnemyCounter;
    [SerializeField] private TextMeshProUGUI txtTimeSecond;
    [SerializeField] private TextMeshProUGUI txtTimeMinute;

    public void Awake()
    {
        EventSystem.current.SetSelectedGameObject(btnStart.gameObject);
    }

    public void Start()
    {
        EventSystem.current.SetSelectedGameObject(btnStart.gameObject);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(btnStart.gameObject);
    }

    public void Update()
    {
        txtEnemyCounter.text = StageResultCounter.enemyKillCount.ToString();
        txtTimeSecond.text = ($"{StageResultCounter.stageTimeSecond.ToString():D2}");
        txtTimeMinute.text = ($"{StageResultCounter.stageTimeMinute.ToString():D2}");

        if (gameObject.activeSelf == true)
        {
            StageResultCounter.playerDontmove = true;
        }

        else
        {
            StageResultCounter.playerDontmove = false;
        }
    }

    public void BtnContinue()
    {

    }

    public void BtnTitle()
    {
        SceneManager.LoadScene(0);
    }
}
