using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRigidbody2D : MonoBehaviour
{
    [Header("LayerMask")]
    [SerializeField] private LayerMask groundCheckLayer;

    [Header("��ڻ�� ���� �̵�")]
    [SerializeField] private float walkSpeed = 5;       //�ȴ� �ӵ�
    [SerializeField] private float runSpeed = 8;        //�ٴ� �ӵ�

    [Header("����")]
    [SerializeField] private float jumpForce = 13;              //���� ��
    [SerializeField] private float lowGravityScale = 2;         //����Ű�� ���� ������ ����Ǵ� �߷�(���� ����)
    [SerializeField] private float highGravityScale = 3.5f;     //�Ϲ� ����

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
        //x�� ���밪�� 0.5�̸� �ȱ�
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
