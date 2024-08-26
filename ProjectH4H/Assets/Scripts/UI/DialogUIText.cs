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

    private void Awake()
    {
        objDialogName.text = dialogTextContainer.dialogName;
        objDialogSaying.text = dialogTextContainer.dialogSaying;

        StartCoroutine(OffTimer());
    }

    private IEnumerator OffTimer()
    {
        yield return new WaitForSeconds(dialogLifeTime);

        gameObject.SetActive(false);
    }

}




