using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour
{
    [Header("�÷��̾� ������")]
    public float Speed = 10.0f;
    float h, v;

    public Animator playerAnim;
    public Transform playerSprite;

    private void Awake()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) 
        {
            playerAnim.SetBool("isRunning", true);
        }

        else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerAnim.SetBool("isRunning", false);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            playerSprite.transform.localScale = new Vector3( -1, 1, 1);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            playerSprite.transform.localScale = new Vector3(1, 1, 1);
        }

        h = Input.GetAxis("Horizontal");        // ������
        v = Input.GetAxis("Vertical");          // ������

        transform.position += new Vector3(h, 0, v) * Speed * Time.deltaTime;
    }
}
