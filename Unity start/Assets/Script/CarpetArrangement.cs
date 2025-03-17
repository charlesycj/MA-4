using UnityEngine;

public class CarpetArrangement : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트 찾기
    public GameObject Carpet1; // 첫 번째 카펫
    public GameObject Carpet2; // 두 번째 카펫 
    bool Arrangement = false; // 카펫 설치 여부 확인
    
    // 방향 지정 확인
    bool CheckW = false; 
    bool CheckA = false; 
    bool CheckD = false; 

    void Update()
    {
        // W 키 눌렀을 때 카펫을 두 개로 나누어 배치
        if (Input.GetKeyDown(KeyCode.W) && Arrangement == false)
        {   CheckA = false; 
            CheckD = false;
            // 카펫을 두 개로 나누어 배치하는 작업
            Vector3 carpetPosition1 = player.position + new Vector3(0, 0, 1); // 첫 번째 카펫의 위치
            Vector3 carpetPosition2 = player.position + new Vector3(0, 0, 2); // 두 번째 카펫의 위치

            // 첫 번째 카펫 위치 설정
            Carpet1.transform.position = carpetPosition1;
            Carpet1.SetActive(true); // 카펫 활성화
            SetTransparency(Carpet1, 0.5f); // 투명도 설정

            // 두 번째 카펫 위치 설정
            Carpet2.transform.position = carpetPosition2;
            Carpet2.SetActive(true); // 카펫 활성화
            SetTransparency(Carpet2, 0.5f); // 투명도 설정

            // W 키 입력 후에만 Q와 E 입력 가능하도록 설정
            CheckW = true;
        }

        // W 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckW && Carpet1 != null && Carpet1.activeSelf && Carpet2 != null && Carpet2.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3 carpetPosition1 = player.position + new Vector3(0, 0, 1); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(-1, 0, 1); // 두 번째 카펫의 위치

                Carpet1.transform.position = carpetPosition1;
                Carpet2.transform.position = carpetPosition2;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 carpetPosition1 = player.position + new Vector3(0, 0, 1); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(1, 0, 1); // 두 번째 카펫의 위치

                Carpet1.transform.position = carpetPosition1;
                Carpet2.transform.position = carpetPosition2;
            }
        }
        
        // A 키 눌렀을 때 카펫을 두 개로 나누어 배치
        if (Input.GetKeyDown(KeyCode.A) && Arrangement == false)
        {
            CheckW = false; 
            CheckD = false;
            // 카펫을 두 개로 나누어 배치하는 작업
            Vector3 carpetPosition1 = player.position + new Vector3(-1, 0, 0); // 첫 번째 카펫의 위치
            Vector3 carpetPosition2 = player.position + new Vector3(-2, 0, 0); // 두 번째 카펫의 위치

            // 첫 번째 카펫 위치 설정
            Carpet1.transform.position = carpetPosition1;
            Carpet1.SetActive(true); // 카펫 활성화
            SetTransparency(Carpet1, 0.5f); // 투명도 설정

            // 두 번째 카펫 위치 설정
            Carpet2.transform.position = carpetPosition2;
            Carpet2.SetActive(true); // 카펫 활성화
            SetTransparency(Carpet2, 0.5f); // 투명도 설정

            // A키 입력 후에만 Q와 E 입력 가능하도록 설정
            CheckA = true;
        }

        // A키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckA && Carpet1 != null && Carpet1.activeSelf && Carpet2 != null && Carpet2.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3 carpetPosition1 = player.position + new Vector3(-1, 0, 0); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(-1, 0, -1); // 두 번째 카펫의 위치

                Carpet1.transform.position = carpetPosition1;
                Carpet2.transform.position = carpetPosition2;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 carpetPosition1 = player.position + new Vector3(-1, 0, 0); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(-1, 0, 1); // 두 번째 카펫의 위치

                Carpet1.transform.position = carpetPosition1;
                Carpet2.transform.position = carpetPosition2;
            }
        }
        
        // D키 눌렀을 때 카펫을 두 개로 나누어 배치
        if (Input.GetKeyDown(KeyCode.D) && Arrangement == false)
        {
            CheckW = false; 
            CheckA = false;
            // 카펫을 두 개로 나누어 배치하는 작업
            Vector3 carpetPosition1 = player.position + new Vector3(1, 0, 0); // 첫 번째 카펫의 위치
            Vector3 carpetPosition2 = player.position + new Vector3(2, 0, 0); // 두 번째 카펫의 위치

            // 첫 번째 카펫 위치 설정
            Carpet1.transform.position = carpetPosition1;
            Carpet1.SetActive(true); // 카펫 활성화
            SetTransparency(Carpet1, 0.5f); // 투명도 설정

            // 두 번째 카펫 위치 설정
            Carpet2.transform.position = carpetPosition2;
            Carpet2.SetActive(true); // 카펫 활성화
            SetTransparency(Carpet2, 0.5f); // 투명도 설정

            // D키 입력 후에만 Q와 E 입력 가능하도록 설정
            CheckD = true;
        }

        // D키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckD && Carpet1 != null && Carpet1.activeSelf && Carpet2 != null && Carpet2.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Vector3 carpetPosition1 = player.position + new Vector3(1, 0, 0); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(1, 0, -1); // 두 번째 카펫의 위치

                Carpet1.transform.position = carpetPosition1;
                Carpet2.transform.position = carpetPosition2;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Vector3 carpetPosition1 = player.position + new Vector3(1, 0, 0); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(1, 0, 1); // 두 번째 카펫의 위치

                Carpet1.transform.position = carpetPosition1;
                Carpet2.transform.position = carpetPosition2;
            }
        }
        // R 키 눌렀을 때 카펫의 투명도를 100%로 설정 (불투명)
        if (Input.GetKeyDown(KeyCode.R) && Arrangement==false)
        {
            if (Carpet1 != null) SetTransparency(Carpet1, 1f);
            if (Carpet2 != null) SetTransparency(Carpet2, 1f);
            Arrangement = true; // 다시 배치할 수 없게 변경
            CheckW = false;
            CheckA = false;
            CheckD = false;
            Debug.Log("카펫 설치 완료");
        }
        
        if (Input.GetKeyDown(KeyCode.F1)) //테스트용코드 f1을 눌러 카펫 다시 설치 
        {
            
            Arrangement = false; // 다시 배치할 수 있게 변경
            
        }
    }

    // 투명도를 설정하는 함수 (알파값을 변경)
    void SetTransparency(GameObject carpet, float alphaValue)
    {
        if (carpet == null) return;

        Renderer carpetRenderer = carpet.GetComponent<Renderer>();
        if (carpetRenderer == null) return;

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
