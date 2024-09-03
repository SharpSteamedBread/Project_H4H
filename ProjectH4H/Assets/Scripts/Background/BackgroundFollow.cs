using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public GameObject targetPosition;
    Vector3 vel = Vector3.zero;

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, 
            new Vector3(targetPosition.transform.position.x, targetPosition.transform.position.y, 0), ref vel, moveSpeed);
    }
}
