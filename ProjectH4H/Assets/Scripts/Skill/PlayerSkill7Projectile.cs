using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill7Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float shootingSpeed = 10f;

    [Header("≈∏∞› ¿Ã∆Â∆Æ")]
    [SerializeField] private GameObject objHitVFXLeft;
    [SerializeField] private GameObject objHitVFXRight;

    private void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        DisableProjectile();
    }
    private void Update()
    {
        if(gameObject.transform.rotation.y == 0)
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
        if(collision.CompareTag("Enemy1") || collision.CompareTag("Enemy2") ||
            collision.CompareTag("Enemy3"))
        {
            Transform hitEffectLocation = collision.GetComponent<Transform>();

            if (hitEffectLocation.localScale.x > 0)
            {
                GameObject cloneHitVFX = Instantiate(objHitVFXRight,
                         new Vector3(hitEffectLocation.position.x, hitEffectLocation.position.y, hitEffectLocation.position.z), Quaternion.identity);
            }

            else if(hitEffectLocation.localScale.x < 0)
            {
                GameObject cloneHitVFX = Instantiate(objHitVFXLeft,
                         new Vector3(-hitEffectLocation.position.x, hitEffectLocation.position.y, hitEffectLocation.position.z), Quaternion.identity);
            }
        }
    }
}
