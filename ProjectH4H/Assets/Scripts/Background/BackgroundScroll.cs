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
    public Transform parentBG5;


    public float speedBG1 = 0.2f;
    public float speedBG2 = 0.4f;
    public float speedBG3 = 0.6f;
    public float speedBG4 = 0.8f;
    public float speedBG5 = 1f;

    // 가상의 스크롤 박스 크기 설정
    public Vector2 scrollBoxSize = new Vector2(20f, 10f);


    private Vector3 previousPlayerPosition;

    void Start()
    {
        previousPlayerPosition = player.position;
    }

    void Update()
    {
        Vector3 deltaMovement = player.position - previousPlayerPosition;

        // X축 이동에 따른 배경 스크롤
        parentBG1.position += Vector3.left * deltaMovement.x * speedBG1;
        parentBG2.position += Vector3.left * deltaMovement.x * speedBG2;
        parentBG3.position += Vector3.left * deltaMovement.x * speedBG3;
        parentBG4.position += Vector3.left * deltaMovement.x * speedBG4;
        parentBG5.position += Vector3.left * deltaMovement.x * speedBG5;


        // Y축 이동에 따른 배경 스크롤 (점프 등)
        parentBG1.position += Vector3.down * deltaMovement.y * speedBG1;
        parentBG2.position += Vector3.down * deltaMovement.y * speedBG2;
        parentBG3.position += Vector3.down * deltaMovement.y * speedBG3;
        parentBG4.position += Vector3.down * deltaMovement.y * speedBG4;
        parentBG5.position += Vector3.down * deltaMovement.y * speedBG5;


        previousPlayerPosition = player.position;



        // 카메라 밖으로 나간 배경을 다시 스크롤링 되도록 재배치
        LoopBackground(parentBG1);
        LoopBackground(parentBG2);
        LoopBackground(parentBG3);
        LoopBackground(parentBG4);
        LoopBackground(parentBG5);
    }

    void LoopBackground(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Vector3 childPos = child.localPosition;

            // X축 방향 재배치
            if (childPos.x < -scrollBoxSize.x / 2)
            {
                childPos.x += scrollBoxSize.x;
            }
            else if (childPos.x > scrollBoxSize.x / 2)
            {
                childPos.x -= scrollBoxSize.x;
            }

            // Y축 방향 재배치
            if (childPos.y < -scrollBoxSize.y / 2)
            {
                childPos.y += scrollBoxSize.y;
            }
            else if (childPos.y > scrollBoxSize.y / 2)
            {
                childPos.y -= scrollBoxSize.y;
            }

            child.localPosition = childPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(scrollBoxSize.x, scrollBoxSize.y, 0));
    }
}
