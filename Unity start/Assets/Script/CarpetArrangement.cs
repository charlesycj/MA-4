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

            // 로컬 기준으로 플레이어 앞쪽에 카펫 배치
            Vector3 carpetPosition1 = player.TransformPoint(Vector3.forward); // 첫 번째 카펫
            Vector3 carpetPosition2 = player.TransformPoint(Vector3.forward * 2); // 두 번째 카펫

            // 카펫 클론 생성
            carpetClone1 = Instantiate(Carpet1_Player1, carpetPosition1, player.rotation);
            carpetClone2 = Instantiate(Carpet2_Player1, carpetPosition2, player.rotation);

            // 클론의 투명도 설정
            SetTransparency(carpetClone1, 0.5f);
            SetTransparency(carpetClone2, 0.5f);

            Arrangement = true;
            CheckW = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && Arrangement)
        {
            CheckA = false;
            CheckD = false;

            // 카펫 위치 업데이트 (플레이어 앞쪽)
            carpetClone1.transform.position = player.TransformPoint(Vector3.forward);
            carpetClone2.transform.position = player.TransformPoint(Vector3.forward * 2);

            CheckW = true;
        }

        if (CheckW)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                carpetClone1.transform.position = player.TransformPoint(Vector3.forward);
                carpetClone2.transform.position = player.TransformPoint(Vector3.forward + Vector3.left);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                carpetClone1.transform.position = player.TransformPoint(Vector3.forward);
                carpetClone2.transform.position = player.TransformPoint(Vector3.forward + Vector3.right);
            }
        }

        // A 키 눌렀을 때 카펫을 왼쪽으로 배치
        if (Input.GetKeyDown(KeyCode.A) && !Arrangement)
        {
            CheckW = false;
            CheckD = false;

            Vector3 carpetPosition1 = player.TransformPoint(Vector3.left);
            Vector3 carpetPosition2 = player.TransformPoint(Vector3.left * 2);

            carpetClone1 = Instantiate(Carpet1_Player1, carpetPosition1, player.rotation);
            carpetClone2 = Instantiate(Carpet2_Player1, carpetPosition2, player.rotation);

            SetTransparency(carpetClone1, 0.5f);
            SetTransparency(carpetClone2, 0.5f);

            Arrangement = true;
            CheckA = true;
        }
        else if (Input.GetKeyDown(KeyCode.A) && Arrangement)
        {
            CheckW = false;
            CheckD = false;

            carpetClone1.transform.position = player.TransformPoint(Vector3.left);
            carpetClone2.transform.position = player.TransformPoint(Vector3.left * 2);

            CheckA = true;
        }

        if (CheckA)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                carpetClone1.transform.position = player.TransformPoint(Vector3.left);
                carpetClone2.transform.position = player.TransformPoint(Vector3.left + Vector3.back);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                carpetClone1.transform.position = player.TransformPoint(Vector3.left);
                carpetClone2.transform.position = player.TransformPoint(Vector3.left + Vector3.forward);
            }
        }

        // D 키 눌렀을 때 카펫을 오른쪽으로 배치
        if (Input.GetKeyDown(KeyCode.D) && !Arrangement)
        {
            CheckW = false;
            CheckA = false;

            Vector3 carpetPosition1 = player.TransformPoint(Vector3.right);
            Vector3 carpetPosition2 = player.TransformPoint(Vector3.right * 2);

            carpetClone1 = Instantiate(Carpet1_Player1, carpetPosition1, player.rotation);
            carpetClone2 = Instantiate(Carpet2_Player1, carpetPosition2, player.rotation);

            SetTransparency(carpetClone1, 0.5f);
            SetTransparency(carpetClone2, 0.5f);

            Arrangement = true;
            CheckD = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && Arrangement)
        {
            CheckW = false;
            CheckA = false;

            carpetClone1.transform.position = player.TransformPoint(Vector3.right);
            carpetClone2.transform.position = player.TransformPoint(Vector3.right * 2);

            CheckD = true;
        }

        if (CheckD)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                carpetClone1.transform.position = player.TransformPoint(Vector3.right);
                carpetClone2.transform.position = player.TransformPoint(Vector3.right + Vector3.forward);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                carpetClone1.transform.position = player.TransformPoint(Vector3.right);
                carpetClone2.transform.position = player.TransformPoint(Vector3.right + Vector3.back);
            }
        }
    }

    // 투명도를 설정하는 함수
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
