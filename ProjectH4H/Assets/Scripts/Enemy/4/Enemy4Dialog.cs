using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Dialog : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip anim1SFX;
    [SerializeField] private AudioClip anim2SFX;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Anim1SFXPlay()
    {
        audioSource.clip = anim1SFX;
        audioSource.PlayOneShot(anim1SFX);
    }

    private void Anim2SFXPlay()
    {
        audioSource.clip = anim2SFX;
        audioSource.PlayOneShot(anim2SFX);
    }

}
