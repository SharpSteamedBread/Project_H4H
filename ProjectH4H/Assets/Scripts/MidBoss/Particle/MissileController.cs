using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float soundDelayTime = 1.0f;

    [SerializeField] private BoxCollider2D objCollider;
    [SerializeField] private ParticleSystem particle;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        objCollider = gameObject.GetComponent<BoxCollider2D>();
        particle = gameObject.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        StartCoroutine(SFXDelay());
        particle.Play();
    }

    private IEnumerator SFXDelay()
    {
        yield return new WaitForSeconds(soundDelayTime);
        audioSource.PlayOneShot(audioClip);
        objCollider.enabled = true;

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
