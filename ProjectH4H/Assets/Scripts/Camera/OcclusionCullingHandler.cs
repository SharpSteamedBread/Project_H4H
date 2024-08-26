using UnityEngine;

public class OcclusionCullingHandler : MonoBehaviour
{
    public Camera mainCamera;

    private void Update()
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

        if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1 || viewportPoint.z < 0)
        {
            // ������Ʈ�� ī�޶��� ���������� �ۿ� ������ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
        else
        {
            // ������Ʈ�� ī�޶��� ���������� �ȿ� ������ Ȱ��ȭ
            gameObject.SetActive(true);
        }
    }
}