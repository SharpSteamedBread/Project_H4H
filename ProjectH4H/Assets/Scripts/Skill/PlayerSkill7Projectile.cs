using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill7Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float shootingSpeed = 10f;

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
}
