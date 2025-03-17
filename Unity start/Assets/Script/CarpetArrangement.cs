using UnityEngine;

public class CarpetArrangement : MonoBehaviour
{
    public GameObject Carpet; // 청사진 오브젝트
    public Transform player; // 플레이어 오브젝트 찾기
    private Renderer carpetRenderer; // 카펫의 Renderer

    void Start()
    {
        // 카펫의 Renderer 컴포넌트를 가져옵니다.
        carpetRenderer = Carpet.GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 carpetPosition = player.position + new Vector3(0, 0, 1);
            Carpet.SetActive(true);
            Carpet.transform.position = carpetPosition;

            // 카펫의 알파값을 50%로 설정 (투명도 50%)
            SetTransparency(0.5f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            // R키를 누르면 카펫의 투명도를 100%로 설정 (불투명)
            SetTransparency(1f);
        }
    }

    // 투명도를 설정하는 함수 (알파값을 변경)
    void SetTransparency(float alphaValue)
    {
        // 기존 색상 가져오기
        Color color = carpetRenderer.material.color;

        // 알파값 수정
        color.a = alphaValue;
        carpetRenderer.material.color = color;

        if (alphaValue == 1f)
        {
            // 불투명 모드 설정 (Opaque)
            carpetRenderer.material.SetFloat("_Mode", 0);
            carpetRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            carpetRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            carpetRenderer.material.SetInt("_ZWrite", 1); // Z-버퍼 활성화
            carpetRenderer.material.renderQueue = -1; // 기본 렌더 큐
        }
        else
        {
            // 투명 모드 설정 (Transparent)
            carpetRenderer.material.SetFloat("_Mode", 3);
            carpetRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            carpetRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            carpetRenderer.material.SetInt("_ZWrite", 0); // Z-버퍼 비활성화
            carpetRenderer.material.renderQueue = 3000; // 투명 객체 렌더 큐

            // 알파 블렌딩 활성화
            carpetRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        }
    }
}