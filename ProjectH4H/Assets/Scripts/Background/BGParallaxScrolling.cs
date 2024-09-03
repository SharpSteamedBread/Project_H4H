using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGParallaxScrolling : MonoBehaviour
{
    public Transform target; // 플레이어 혹은 카메라
    public float parallaxFactor; // 패럴랙스 효과의 강도 (0에 가까울수록 덜 움직임)

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
