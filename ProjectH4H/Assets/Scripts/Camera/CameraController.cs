using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothing = 0.2f;
    [SerializeField] Vector2 minCameraBoundary;
    [SerializeField] Vector2 maxCameraBoundary;

    private float targetX = 32.5f; // 목표 X 좌표값
    private float moveSpeed = 2f; // 카메라 이동 속도

    [SerializeField] private float valueControlY = 0;


    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(player.position.x, player.position.y + valueControlY, this.transform.position.z);

        //targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        //targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
