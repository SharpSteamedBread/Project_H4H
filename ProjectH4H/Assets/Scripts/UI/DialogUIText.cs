using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



[System.Serializable]
public class DialogTextConatiner
{
    public string dialogName;
    public string dialogSaying;
}

public class DialogUIText : MonoBehaviour
{
    public TextMeshProUGUI objDialogName;
    public TextMeshProUGUI objDialogSaying;

    public DialogTextConatiner dialogTextContainer;
    //public List<DialogTextConatiner> DialogTextContainerList = new List<DialogTextConatiner>();

    [SerializeField] private float dialogLifeTime = 10f;

    [Header("스프라이트 교환")]
    [SerializeField] private Image objImageBackground;
    [SerializeField] private Sprite imgOs;
    [SerializeField] private Sprite imgMos;

    private void Awake()
    {
        objDialogName.text = dialogTextContainer.dialogName;
        objDialogSaying.text = dialogTextContainer.dialogSaying;

        StartCoroutine(OffTimer());
    }

    private void Update()
    {
        if(objDialogName.text == "오스")
        {
            objImageBackground.sprite = imgOs;
        }

        else if (objDialogName.text == "모스")
        {
            objImageBackground.sprite = imgMos;
        }
    }

    private IEnumerator OffTimer()
    {
        yield return new WaitForSeconds(dialogLifeTime);

        gameObject.SetActive(false);
    }

}




