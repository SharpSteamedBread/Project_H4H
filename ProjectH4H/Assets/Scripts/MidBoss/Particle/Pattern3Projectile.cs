using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3Projectile : MonoBehaviour
{
    /*
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float soundDelayTime = 1.0f;
    */

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float shootingSpeed = 10f;

    [SerializeField] private GameObject vfxHit;


    private void Awake()
    {
        //audioSource = gameObject.GetComponent<AudioSource>();

        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        DisableProjectile();
    }

    private void Update()
    {
        if (gameObject.transform.rotation.y == 0)
        {
            rigidBody.AddForce(Vector3.right * shootingSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }

        else
        {
            rigidBody.AddForce(Vector3.left * shootingSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private IEnumerator DisableProjectile()
    {
        yield return new WaitForSeconds(1.5f);

        gameObject.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Transform hitEffectLocation = collision.GetComponent<Transform>();

            if (hitEffectLocation.localScale.x > 0)
            {
                GameObject cloneHitVFX = Instantiate(vfxHit,
                         new Vector3(hitEffectLocation.position.x, hitEffectLocation.position.y, hitEffectLocation.position.z), Quaternion.identity);
                cloneHitVFX.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
