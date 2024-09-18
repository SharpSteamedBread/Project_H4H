using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform

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

        // X축 이동에 따른 배경 스크롤
        parentBG1.position += Vector3.left * deltaMovement.x * speedBG1;
        parentBG2.position += Vector3.left * deltaMovement.x * speedBG2;
        parentBG3.position += Vector3.left * deltaMovement.x * speedBG3;
        parentBG4.position += Vector3.left * deltaMovement.x * speedBG4;

        // Y축 이동에 따른 배경 스크롤 (점프 등)
        parentBG1.position += Vector3.down * deltaMovement.y * speedBG1;
        parentBG2.position += Vector3.down * deltaMovement.y * speedBG2;
        parentBG3.position += Vector3.down * deltaMovement.y * speedBG3;
        parentBG4.position += Vector3.down * deltaMovement.y * speedBG4;

        previousPlayerPosition = player.position;

        // 카메라 밖으로 나간 배경을 다시 스크롤링 되도록 재배치
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

            // X축 방향으로 카메라 밖으로 나갔을 때 재배치
            if (viewPos.x < 0f)  // 왼쪽 경계
            {
                childPos.x += Camera.main.orthographicSize * 2 * Camera.main.aspect;  // 화면 너비만큼 오른쪽으로 이동
            }
            else if (viewPos.x > 1f)  // 오른쪽 경계
            {
                childPos.x -= Camera.main.orthographicSize * 2 * Camera.main.aspect;  // 화면 너비만큼 왼쪽으로 이동
            }

            // Y축 방향으로 카메라 밖으로 나갔을 때 재배치
            if (viewPos.y < 0f)  // 아래 경계
            {
                childPos.y += Camera.main.orthographicSize * 2;  // 화면 높이만큼 위로 이동
            }
            else if (viewPos.y > 1f)  // 위 경계
            {
                childPos.y -= Camera.main.orthographicSize * 2;  // 화면 높이만큼 아래로 이동
            }

            child.position = childPos;
        }
    }
}
