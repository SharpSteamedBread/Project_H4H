using UnityEngine;

public class ParallaxEffect0919 : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] float distance;                //카메라의 현재 위치와 시작 위치 간 거리

    [Range(0.1f, 2.0f)]
    public float speed = 0.2f;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        distance += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }

}