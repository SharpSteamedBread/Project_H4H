using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSector : MonoBehaviour
{
    [SerializeField] private DialogText dialogText;
    [SerializeField] private int dialogStartPointValue;
    [SerializeField] private int dialogEndPointValue;
    [SerializeField] private BoxCollider2D collider;

    private void Awake()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            dialogText.dialogStartPoint = dialogStartPointValue;
            dialogText.dialogEndPoint = dialogEndPointValue;
            collider.enabled = false;
            StartCoroutine(DisableControler());
        }
    }

    private IEnumerator DisableControler()
    {
        yield return StartCoroutine(dialogText.TutorialDialog());

        gameObject.SetActive(false);
    }
}
