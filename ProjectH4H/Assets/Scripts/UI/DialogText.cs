using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private float pauseTime = 3.0f;

    public int dialogStartPoint;
    public int dialogEndPoint;

    [Header("튜토리얼 옵션")]
    [SerializeField] private GameObject objBlock;
    //[SerializeField] private bool isTutStopped;

    private void Awake()
    {
        dialogStartPoint = 0;
        dialogEndPoint = 5;
        //isTutStopped = false;
    }

    private void Start()
    {
        StartCoroutine(TutorialDialog());

        /*
        if(SceneManager.GetActiveScene().name == "Map_Tutorial")
        {
            StartCoroutine(TutorialDialog());
        }
        */
    }

    private void Update()
    {
        /*
        if (isTutStopped = true && SceneManager.GetActiveScene().name == "Map_Tutorial")
        {
            objBlock.SetActive(false);
        }
        */
    }

    public IEnumerator TutorialDialog()
    {

        for(int i = dialogStartPoint; i <= dialogEndPoint; i++)
        {
            GameObject cloneDialog = Instantiate(objDialogUI, objDialogUI.transform.position, objDialogUI.transform.rotation);
            cloneDialog.transform.SetParent(objDialogParent.transform);

            DialogTextConatiner cloneDialogTextContainer = new DialogTextConatiner();
            cloneDialogTextContainer.dialogName = DialogTextTutorialList[i].dialogName;
            cloneDialogTextContainer.dialogSaying = DialogTextTutorialList[i].dialogSaying;

            cloneDialog.GetComponent<DialogUIText>().dialogTextContainer = cloneDialogTextContainer;

            cloneDialog.GetComponent<DialogUIText>().objDialogName.text = DialogTextTutorialList[i].dialogName;
            cloneDialog.GetComponent<DialogUIText>().objDialogSaying.text = DialogTextTutorialList[i].dialogSaying;

            yield return new WaitForSeconds(pauseTime);
        }

        //isTutStopped = true;

    }
}
