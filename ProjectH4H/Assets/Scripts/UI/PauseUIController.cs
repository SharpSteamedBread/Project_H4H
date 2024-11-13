using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUIController : MonoBehaviour
{
    [SerializeField] private bool onoffUI;
    [SerializeField] private GameObject objUI;

    [Header("UI 포커스 초기화")]
    [SerializeField] private Button btnResume;

    [Header("설정")]
    [SerializeField] private GameObject objUISetting;
    [SerializeField] private Button btnSetting;

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(btnResume.gameObject);
    }

    void Start()
    {
        onoffUI = true;
        EventSystem.current.SetSelectedGameObject(btnResume.gameObject);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(btnResume.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        StageResultCounter.playerDontmove = !onoffUI;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            onoffUI = !onoffUI;
            objUI.SetActive(!onoffUI);
        }

        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            if (objUI.activeSelf == true)
            {
                Time.timeScale = 0;
            }

            else
            {
                Time.timeScale = 1;
            }
        }

        else
        {
            Destroy(gameObject);
        }

        Debug.Log($"현재 시간 속도: {Time.timeScale}, playerDontMove static 변수: {StageResultCounter.playerDontmove}");
    }

    public void ButtonResume()
    {
        onoffUI = true;
        objUI.SetActive(false);
    }

    public void ButtonSetting()
    {
        objUISetting.SetActive(true);
    }

    public void ButtonTitle()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ButtonExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
