using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogManager : MonoBehaviour
{
    [Header("기본 지역")]
    public TextAsset dialogMidbossFile;
    public DialogList dialogList01;
 

    private void Awake()
    {
        LoadDialog01();
    }

    private void LoadDialog01()
    {
        string json = "{\"dialogMidboss\":" + dialogMidbossFile.text + "}";
        dialogList01 = JsonUtility.FromJson<DialogList>(json);
    }


    void DisplayDialog(int dialogNumber)
    {
        if(dialogNumber < dialogList01.dialogMidboss.Count)
        {
            DialogMidboss dialog01 = dialogList01.dialogMidboss[dialogNumber];
            Debug.Log(dialog01.charDialog);
        }
    }
}
