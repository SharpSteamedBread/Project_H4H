using UnityEngine;

public class ParallaxEffect0919 : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Transform player;  // �÷��̾��� Transform�� ����
    [SerializeField] float parallaxFactor = 0.1f; // �÷��̾� �̵� �� ����� �����̴� ����

    private Vector3 previousPlayerPos;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        previousPlayerPos = player.position; // ������ �� �÷��̾� ��ġ ����
    }

    private void Update()
    {
        Vector3 deltaMovement = player.position - previousPlayerPos; // �÷��̾��� �̵� ���� ���
        float distance = deltaMovement.x * parallaxFactor; // ��� ��ũ�� �ӵ� ����
        mat.SetTextureOffset("_MainTex", mat.mainTextureOffset + Vector2.right * distance); // �ؽ�ó ������ ����

        previousPlayerPos = player.position; // ���� �÷��̾� ��ġ�� ���� ��ġ�� ������Ʈ
    }
}
