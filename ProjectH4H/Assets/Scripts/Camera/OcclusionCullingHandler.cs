using UnityEngine;

public class OcclusionCullingHandler : MonoBehaviour
{
    public Camera mainCamera;

    private void Update()
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1 || viewportPoint.z < 0)
        {
            // 오브젝트가 카메라의 뷰프러스텀 밖에 있으면 비활성화
            gameObject.SetActive(false);
        }
        else
        {
            // 오브젝트가 카메라의 뷰프러스텀 안에 있으면 활성화
            gameObject.SetActive(true);
        }
    }
}