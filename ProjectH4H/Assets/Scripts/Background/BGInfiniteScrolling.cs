using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGInfiniteScrolling : MonoBehaviour
{
    public float backgroundWidth; // ��� �̹����� �ʺ�
    public Transform cameraTransform; // ī�޶��� Transform

    private Transform[] backgrounds;
    private Vector3 previousCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        previousCameraPosition = cameraTransform.position;

        // ����� ��� ã�� �迭�� ����
        backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }
    }

    void Update()
    {
        // ī�޶� ������ �Ÿ���ŭ ����� �̵�
        Vector3 deltaMovement = cameraTransform.position - previousCameraPosition;
        foreach (Transform background in backgrounds)
        {
            background.position += new Vector3(deltaMovement.x, 0, 0);
        }

        previousCameraPosition = cameraTransform.position;

        // ����� ī�޶� ������ ������� Ȯ��
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
        // ����� ���������� ��ġ�Ͽ� ���� ��ũ�� ȿ�� ����
        Vector3 backgroundOffset = new Vector3(backgroundWidth * backgrounds.Length, 0, 0);
        background.position += backgroundOffset;
    }
}
