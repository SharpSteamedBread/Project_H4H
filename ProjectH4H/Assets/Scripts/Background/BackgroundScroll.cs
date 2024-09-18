using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform

    public Transform parentBG1;
    public Transform parentBG2;
    public Transform parentBG3;
    public Transform parentBG4;

    public float speedBG1 = 0.2f;
    public float speedBG2 = 0.4f;
    public float speedBG3 = 0.6f;
    public float speedBG4 = 0.8f;

    private Vector3 previousPlayerPosition;

    void Start()
    {
        previousPlayerPosition = player.position;
    }

    void FixedUpdate()
    {
        Vector3 deltaMovement = player.position - previousPlayerPosition;

        // X�� �̵��� ���� ��� ��ũ��
        parentBG1.position += Vector3.left * deltaMovement.x * speedBG1;
        parentBG2.position += Vector3.left * deltaMovement.x * speedBG2;
        parentBG3.position += Vector3.left * deltaMovement.x * speedBG3;
        parentBG4.position += Vector3.left * deltaMovement.x * speedBG4;

        // Y�� �̵��� ���� ��� ��ũ�� (���� ��)
        parentBG1.position += Vector3.down * deltaMovement.y * speedBG1;
        parentBG2.position += Vector3.down * deltaMovement.y * speedBG2;
        parentBG3.position += Vector3.down * deltaMovement.y * speedBG3;
        parentBG4.position += Vector3.down * deltaMovement.y * speedBG4;

        previousPlayerPosition = player.position;

        // ī�޶� ������ ���� ����� �ٽ� ��ũ�Ѹ� �ǵ��� ���ġ
        LoopBackground(parentBG1);
        LoopBackground(parentBG2);
        LoopBackground(parentBG3);
        LoopBackground(parentBG4);
    }

    void LoopBackground(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Vector3 viewPos = Camera.main.WorldToViewportPoint(child.position);
            Vector3 childPos = child.position;

            // X�� �������� ī�޶� ������ ������ �� ���ġ
            if (viewPos.x < 0f)  // ���� ���
            {
                childPos.x += Camera.main.orthographicSize * 2 * Camera.main.aspect;  // ȭ�� �ʺ�ŭ ���������� �̵�
            }
            else if (viewPos.x > 1f)  // ������ ���
            {
                childPos.x -= Camera.main.orthographicSize * 2 * Camera.main.aspect;  // ȭ�� �ʺ�ŭ �������� �̵�
            }

            // Y�� �������� ī�޶� ������ ������ �� ���ġ
            if (viewPos.y < 0f)  // �Ʒ� ���
            {
                childPos.y += Camera.main.orthographicSize * 2;  // ȭ�� ���̸�ŭ ���� �̵�
            }
            else if (viewPos.y > 1f)  // �� ���
            {
                childPos.y -= Camera.main.orthographicSize * 2;  // ȭ�� ���̸�ŭ �Ʒ��� �̵�
            }

            child.position = childPos;
        }
    }
}
