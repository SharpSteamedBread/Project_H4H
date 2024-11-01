using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float soundDelayTime = 1.0f;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(SFXDelay());
    }

    private IEnumerator SFXDelay()
    {
        yield return new WaitForSeconds(soundDelayTime);
        audioSource.PlayOneShot(audioClip);


    }
}
