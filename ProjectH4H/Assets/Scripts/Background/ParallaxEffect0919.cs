using UnityEngine;

public class ParallaxEffect0919 : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] float distance;                //ī�޶��� ���� ��ġ�� ���� ��ġ �� �Ÿ�

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