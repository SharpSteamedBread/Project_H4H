using UnityEngine;

public class ParallaxEffect0919 : MonoBehaviour
{
    [SerializeField] Material mat;
    [SerializeField] Transform player;  // 플레이어의 Transform을 참조
    [SerializeField] float parallaxFactor = 0.1f; // 플레이어 이동 시 배경이 움직이는 정도

    private Vector3 previousPlayerPos;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        previousPlayerPos = player.position; // 시작할 때 플레이어 위치 저장
    }

    private void Update()
    {
        Vector3 deltaMovement = player.position - previousPlayerPos; // 플레이어의 이동 차이 계산
        float distance = deltaMovement.x * parallaxFactor; // 배경 스크롤 속도 조정
        mat.SetTextureOffset("_MainTex", mat.mainTextureOffset + Vector2.right * distance); // 텍스처 오프셋 변경

        previousPlayerPos = player.position; // 현재 플레이어 위치를 이전 위치로 업데이트
    }
}
