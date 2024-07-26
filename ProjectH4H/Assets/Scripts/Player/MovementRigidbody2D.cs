using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRigidbody2D : MonoBehaviour
{
    [Header("LayerMask")]
    [SerializeField] private LayerMask groundCheckLayer;

    [Header("고박사님 버전 이동")]
    [SerializeField] private float walkSpeed = 5;       //걷는 속도
    [SerializeField] private float runSpeed = 8;        //뛰는 속도

    [Header("점프")]
    [SerializeField] private float jumpForce = 13;              //점프 힘
    [SerializeField] private float lowGravityScale = 2;         //점프키를 오래 누르면 적용되는 중력(높은 점프)
    [SerializeField] private float highGravityScale = 3.5f;     //일반 점프

    private float moveSpeed;

    private Rigidbody2D rigid2D;
    private Collider2D collider2D;

    public bool IsLongJump { set; get; } = false;
    public bool IsGrounded { private set; get; } = false;


    private void Awake()
    {
        moveSpeed = walkSpeed;

        rigid2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }


    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void MoveTo(float x)
    {
        //x의 절대값이 0.5이면 걷기
        moveSpeed = Mathf.Abs(x) != 1 ? walkSpeed : runSpeed;
        if (x != 0) x = Mathf.Sign(x);
        rigid2D.velocity = new Vector2(x * moveSpeed, rigid2D.velocity.y);
    }

    public void Jump()
    {
        if(IsGrounded == true)
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpForce);
        }
    }

    private void JumpHeight()
    {
        if(IsLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = lowGravityScale;
        }
        else
        {
            rigid2D.gravityScale = highGravityScale;
        }
    }
}
