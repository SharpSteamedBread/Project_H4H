using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSector : MonoBehaviour
{
    [SerializeField] private DialogText dialogText;
    [SerializeField] private int dialogStartPointValue;
    [SerializeField] private int dialogEndPointValue;
    [SerializeField] private BoxCollider2D tutorialTrigger;

    [SerializeField] private GameObject blockBoundary;

    private void Awake()
    {
        tutorialTrigger = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            dialogText.dialogStartPoint = dialogStartPointValue;
            dialogText.dialogEndPoint = dialogEndPointValue;
            tutorialTrigger.enabled = false;
            StartCoroutine(DisableControler());
        }
    }

    private IEnumerator DisableControler()
    {
        yield return StartCoroutine(dialogText.TutorialDialog());

        blockBoundary.SetActive(false);
        gameObject.SetActive(false);
    }
}
