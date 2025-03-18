using UnityEngine;

public class CarpetArrangement : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트 참조

    bool Arrangement = false; // 카펫 배치 여부
    bool CheckW = false, CheckA = false, CheckD = false; // 각 키 입력 여부 확인

    // 각 카펫 프리팹
    public GameObject Carpet1_Player1;
    public GameObject Carpet2_Player1;

    // 카펫 클론 저장 변수
    private GameObject carpetClone1;
    private GameObject carpetClone2;

    void Update()
    {
        // W 키 눌렀을 때 카펫을 두 개로 나누어 배치 (처음에만)
        if (Input.GetKeyDown(KeyCode.W) && !Arrangement)
        {
            CheckA = false;
            CheckD = false;

            // 카펫을 두 개로 나누어 배치하는 작업
            Vector3 carpetPosition1 = player.position + new Vector3(0, 0, 1); // 첫 번째 카펫의 위치
            Vector3 carpetPosition2 = player.position + new Vector3(0, 0, 2); // 두 번째 카펫의 위치

            // 카펫 클론 생성
            carpetClone1 = Instantiate(Carpet1_Player1, carpetPosition1, Quaternion.identity);
            carpetClone2 = Instantiate(Carpet2_Player1, carpetPosition2, Quaternion.identity);

            // 클론의 투명도 설정
            SetTransparency(carpetClone1, 0.5f);
            SetTransparency(carpetClone2, 0.5f);

            Arrangement = true; // 카펫 배치 상태 변경

            // W 키 입력 후에만 Q와 E 입력 가능하도록 설정
            CheckW = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && Arrangement)
        {
            CheckA = false;
            CheckD = false;
            // 카펫이 이미 배치되어 있으면 위치만 업데이트
            Vector3 carpetPosition1 = player.position + new Vector3(0, 0, 1); // 첫 번째 카펫의 위치
            Vector3 carpetPosition2 = player.position + new Vector3(0, 0, 2); // 두 번째 카펫의 위치

            carpetClone1.transform.position = carpetPosition1;
            carpetClone2.transform.position = carpetPosition2;
        }

        // W 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckW)
        {
            CheckA = false;
            CheckD = false;
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // W 키에 따른 Q 위치로 이동
                Vector3 carpetPosition1 = player.position + new Vector3(0, 0, 1); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(-1, 0, 1); // 두 번째 카펫의 위치

                // 기존 카펫 위치만 이동
                carpetClone1.transform.position = carpetPosition1;
                carpetClone2.transform.position = carpetPosition2;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                // W 키에 따른 E 위치로 이동
                Vector3 carpetPosition1 = player.position + new Vector3(0, 0, 1); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(1, 0, 1); // 두 번째 카펫의 위치

                // 기존 카펫 위치만 이동
                carpetClone1.transform.position = carpetPosition1;
                carpetClone2.transform.position = carpetPosition2;
            }
        }

        // A 키 눌렀을 때 카펫을 두 개로 나누어 배치 (처음에만)
        if (Input.GetKeyDown(KeyCode.A) && !Arrangement)
        {
            CheckW = false;
            CheckD = false;

            // 카펫을 두 개로 나누어 배치하는 작업
            Vector3 carpetPosition1 = player.position + new Vector3(-1, 0, 0); // 첫 번째 카펫의 위치
            Vector3 carpetPosition2 = player.position + new Vector3(-2, 0, 0); // 두 번째 카펫의 위치

            // 카펫 클론 생성
            carpetClone1 = Instantiate(Carpet1_Player1, carpetPosition1, Quaternion.identity);
            carpetClone2 = Instantiate(Carpet2_Player1, carpetPosition2, Quaternion.identity);

            // 클론의 투명도 설정
            SetTransparency(carpetClone1, 0.5f);
            SetTransparency(carpetClone2, 0.5f);

            Arrangement = true; // 카펫 배치 상태 변경

            // A 키 입력 후에만 Q와 E 입력 가능하도록 설정
            CheckA = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && Arrangement)
        {
            CheckW = false;
            CheckD = false;
            // 카펫이 이미 배치되어 있으면 위치만 업데이트
            Vector3 carpetPosition1 = player.position + new Vector3(-1, 0, 0); // 첫 번째 카펫의 위치
            Vector3 carpetPosition2 = player.position + new Vector3(-2, 0, 0); // 두 번째 카펫의 위치

            carpetClone1.transform.position = carpetPosition1;
            carpetClone2.transform.position = carpetPosition2;
            CheckA = true;
        }

        
        // A 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckA)
        {
           
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // W 키에 따른 Q 위치로 이동
                Vector3 carpetPosition1 = player.position + new Vector3(-1, 0, 0); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(-1, 0, -1); // 두 번째 카펫의 위치

                // 기존 카펫 위치만 이동
                carpetClone1.transform.position = carpetPosition1;
                carpetClone2.transform.position = carpetPosition2;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                // W 키에 따른 E 위치로 이동
                Vector3 carpetPosition1 = player.position + new Vector3(-1, 0, 0); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(-1, 0, 1); // 두 번째 카펫의 위치

                // 기존 카펫 위치만 이동
                carpetClone1.transform.position = carpetPosition1;
                carpetClone2.transform.position = carpetPosition2;
            }
        }

        // D 키 눌렀을 때 카펫을 두 개로 나누어 배치 (처음에만)
        if (Input.GetKeyDown(KeyCode.D) && !Arrangement)
        {
            CheckW = false;
            CheckA = false;

            // 카펫을 두 개로 나누어 배치하는 작업
            Vector3 carpetPosition1 = player.position + new Vector3(1, 0, 0); // 첫 번째 카펫의 위치
            Vector3 carpetPosition2 = player.position + new Vector3(2, 0, 0); // 두 번째 카펫의 위치

            // 카펫 클론 생성
            carpetClone1 = Instantiate(Carpet1_Player1, carpetPosition1, Quaternion.identity);
            carpetClone2 = Instantiate(Carpet2_Player1, carpetPosition2, Quaternion.identity);

            // 클론의 투명도 설정
            SetTransparency(carpetClone1, 0.5f);
            SetTransparency(carpetClone2, 0.5f);

            Arrangement = true; // 카펫 배치 상태 변경

            // D 키 입력 후에만 Q와 E 입력 가능하도록 설정
            CheckD = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && Arrangement)
        {
            CheckW = false;
            CheckA = false;
            // 카펫이 이미 배치되어 있으면 위치만 업데이트
            Vector3 carpetPosition1 = player.position + new Vector3(1, 0, 0); // 첫 번째 카펫의 위치
            Vector3 carpetPosition2 = player.position + new Vector3(2, 0, 0); // 두 번째 카펫의 위치

            carpetClone1.transform.position = carpetPosition1;
            carpetClone2.transform.position = carpetPosition2;
            CheckD = true;
        }

        // D 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckD)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // D 키에 따른 Q 위치로 이동
                Vector3 carpetPosition1 = player.position + new Vector3(1, 0, 0); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(1, 0, 1); // 두 번째 카펫의 위치

                // 기존 카펫 위치만 이동
                carpetClone1.transform.position = carpetPosition1;
                carpetClone2.transform.position = carpetPosition2;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                // D 키에 따른 E 위치로 이동
                Vector3 carpetPosition1 = player.position + new Vector3(1, 0, 0); // 첫 번째 카펫의 위치
                Vector3 carpetPosition2 = player.position + new Vector3(1, 0, -1); // 두 번째 카펫의 위치

                // 기존 카펫 위치만 이동
                carpetClone1.transform.position = carpetPosition1;
                carpetClone2.transform.position = carpetPosition2;
            }
        }
    }

    // 투명도를 설정하는 함수 (알파값을 변경)
    void SetTransparency(GameObject carpet, float alphaValue)
    {
        if (carpet == null) return;

        Renderer carpetRenderer = carpet.GetComponent<Renderer>();
        if (carpetRenderer == null) return;

        Color color = carpetRenderer.material.color;
        color.a = alphaValue;
        carpetRenderer.material.color = color;
    }
}
