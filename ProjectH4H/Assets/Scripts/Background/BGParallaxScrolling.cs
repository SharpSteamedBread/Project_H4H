using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParallaxScrolling : MonoBehaviour
{
    public Transform target; // �÷��̾� Ȥ�� ī�޶�
    public float parallaxFactor; // �з����� ȿ���� ���� (0�� �������� �� ������)

    public Transform bgTransform;

    private Vector3 previousPosition;

    private void Awake()
    {
        bgTransform = gameObject.GetComponent<Transform>();
    }

    void Start()
    {
        previousPosition = new Vector3(target.position.x, bgTransform.position.y, target.position.z);
    }

    void Update()
    {
        Vector3 deltaMovement = target.position - previousPosition;
        transform.position += deltaMovement * parallaxFactor;
        previousPosition = target.position;
    }
}
