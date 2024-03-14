using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandCheck : MonoBehaviour
{
    public GameObject objCommandKeyQ;
    public GameObject objCommandKeyE;
    public GameObject objCommandKeyA;
    public GameObject objCommandKeyD;

    public GameObject assortObjCommandKey;

    public GameObject objCommandEnterUI;

    public string inputCommand;

    public Animator animPlayer;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objCommandEnterUI.activeSelf == true)
        {
            CorrectCommand();

            if (Input.GetKeyDown(KeyCode.Q))
            {
                GameObject cloneQ = Instantiate(objCommandKeyQ, objCommandKeyQ.transform.position, objCommandKeyQ.transform.rotation);
                cloneQ.transform.SetParent(assortObjCommandKey.transform);
                cloneQ.transform.localScale = Vector3.one * 0.7f;
                inputCommand = inputCommand + "Q";
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject cloneE = Instantiate(objCommandKeyE, objCommandKeyE.transform.position, objCommandKeyE.transform.rotation);
                cloneE.transform.SetParent(assortObjCommandKey.transform);
                cloneE.transform.localScale = Vector3.one * 0.7f;
                inputCommand = inputCommand + "E";
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                GameObject cloneA = Instantiate(objCommandKeyA, objCommandKeyA.transform.position, objCommandKeyA.transform.rotation);
                cloneA.transform.SetParent(assortObjCommandKey.transform);
                cloneA.transform.localScale = Vector3.one * 0.7f;
                inputCommand = inputCommand + "A";
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                GameObject cloneD = Instantiate(objCommandKeyD, objCommandKeyD.transform.position, objCommandKeyD.transform.rotation);
                cloneD.transform.SetParent(assortObjCommandKey.transform);
                cloneD.transform.localScale = Vector3.one * 0.7f;
                inputCommand = inputCommand + "D";
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inputCommand = "";

                foreach (Transform child in assortObjCommandKey.transform)
                {
                    Destroy(child.gameObject);
                }

                gameObject.GetComponent<CommandEnterUI>().isCommandUIOpen = gameObject.GetComponent<CommandEnterUI>().isCommandUIOpen!;
            }
        }
    }

    private void CorrectCommand()
    {
        if(inputCommand == "EEQQ")
        {
            animPlayer.SetTrigger("doAttack");
            inputCommand = "";


            foreach (Transform child in assortObjCommandKey.transform)
            {
                Destroy(child.gameObject);
            }

            gameObject.GetComponent<CommandEnterUI>().isCommandUIOpen = false;


            objCommandEnterUI.SetActive(false);
        }
    }
}