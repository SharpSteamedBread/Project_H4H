using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandEnterUI : MonoBehaviour
{
    public GameObject objCommandEnterUI;

    public bool isCommandUIOpen = false;

    private void Awake()
    {
    }

   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isCommandUIOpen = !isCommandUIOpen;
            objCommandEnterUI.SetActive(isCommandUIOpen!);
        }
    }


}
