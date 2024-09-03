using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGInfiniteScrolling : MonoBehaviour
{
    public float backgroundWidth; // 배경 이미지의 너비
    public Transform cameraTransform; // 카메라의 Transform

    private Transform[] backgrounds;
    private Vector3 previousCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        previousCameraPosition = cameraTransform.position;

        // 배경을 모두 찾아 배열에 넣음
        backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        // 카메라가 움직인 거리만큼 배경을 이동
        Vector3 deltaMovement = cameraTransform.position - previousCameraPosition;
        foreach (Transform background in backgrounds)
        {
            background.position += new Vector3(deltaMovement.x, 0, 0);
        }

        previousCameraPosition = cameraTransform.position;

        // 배경이 카메라 밖으로 벗어났는지 확인
        foreach (Transform background in backgrounds)
        {
            if (cameraTransform.position.x - background.position.x > backgroundWidth)
            {
                RepositionBackground(background);
            }
        }
    }

    void RepositionBackground(Transform background)
    {
        // 배경을 오른쪽으로 배치하여 무한 스크롤 효과 구현
        Vector3 backgroundOffset = new Vector3(backgroundWidth * backgrounds.Length, 0, 0);
        background.position += backgroundOffset;
    }
}
