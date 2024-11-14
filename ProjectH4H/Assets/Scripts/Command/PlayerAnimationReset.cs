using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationReset : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        Debug.Log("된");
        animator.enabled = false;
        Debug.Log("다");
        animator.enabled = true;
        Debug.Log("고");
    }

    private void OnDisable()
    {
        Debug.Log("된");
        animator.enabled = false;
        Debug.Log("다");
        animator.enabled = true;
        Debug.Log("고");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
