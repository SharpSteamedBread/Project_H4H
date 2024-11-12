using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainSceneButton : MonoBehaviour
{
    [SerializeField] private Button btnStart;
    [SerializeField] private GameObject objUISetting;

    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(btnStart.gameObject);
    }

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(btnStart.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button_GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void Button_GameSetting()
    {
        objUISetting.SetActive(true);
    }

    public void Button_GameQuit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
        Application.Quit();

#endif
    }
}
