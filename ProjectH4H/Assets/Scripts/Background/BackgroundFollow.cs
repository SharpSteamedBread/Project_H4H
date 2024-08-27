using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public GameObject targetPosition;
    Vector3 vel = Vector3.zero;

    [SerializeField] private float moveSpeed = 10f;

    private void Awake()
    {
        moveSpeed = 10f;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPosition.transform.position, ref vel, moveSpeed);
    }
}
