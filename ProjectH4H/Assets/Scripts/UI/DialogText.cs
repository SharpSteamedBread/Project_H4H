using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogTextEnter
{
    public string dialogName;
    public string dialogSaying;
}


public class DialogText : MonoBehaviour
{
    public List<DialogTextEnter> DialogTextTutorialList = new List<DialogTextEnter>();
    public List<DialogTextEnter> DialogTextObjectList = new List<DialogTextEnter>();
    public List<DialogTextEnter> DialogTextBossList = new List<DialogTextEnter>();

    public GameObject objDialogUI;
    public GameObject objDialogParent;

    [SerializeField] private float pauseTime = 7.0f;

    private void Awake()
    {
        
    }

    private void Start()
    {
        StartCoroutine(TutorialDialog());
    }

    private IEnumerator TutorialDialog()
    {

        for(int i = 0; i < DialogTextTutorialList.Count; i++)
        {
            GameObject cloneDialog = Instantiate(objDialogUI, objDialogUI.transform.position, objDialogUI.transform.rotation);
            cloneDialog.transform.SetParent(objDialogParent.transform);

            //DialogTextConatiner cloneDialogTextContainerName = cloneDialog.GetComponent<DialogTextConatiner>();
            //cloneDialogTextContainerName.dialogName = DialogTextTutorialList[i].dialogName;
            //cloneDialogTextContainerName.dialogSaying = DialogTextTutorialList[i].dialogSaying;


            //cloneDialog.GetComponent<DialogTextConatiner>().dialogName = DialogTextTutorialList[i].dialogName;
            //cloneDialog.GetComponent<DialogTextConatiner>().dialogSaying = DialogTextTutorialList[i].dialogSaying;

            yield return new WaitForSeconds(pauseTime);
        }

    }
}
