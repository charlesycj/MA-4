using UnityEngine;

public class CarpetArrangement : MonoBehaviour
{
    public Transform[] players; // 4명의 플레이어
    private int currentPlayerIndex = 0; // 현재 차례인 플레이어 인덱스
    private bool[] playerArrangements = new bool[4]; // 각 플레이어가 카펫을 배치했는지 여부
    private GameObject[][] carpetClones = new GameObject[4][]; // 각 플레이어의 카펫 클론

    // 각 플레이어마다 카펫 프리팹을 할당할 수 있도록
    public GameObject Carpet1_Player1; // 플레이어 1의 첫 번째 카펫
    public GameObject Carpet2_Player1; // 플레이어 1의 두 번째 카펫
    public GameObject Carpet1_Player2; // 플레이어 2의 첫 번째 카펫
    public GameObject Carpet2_Player2; // 플레이어 2의 두 번째 카펫
    public GameObject Carpet1_Player3; // 플레이어 3의 첫 번째 카펫
    public GameObject Carpet2_Player3; // 플레이어 3의 두 번째 카펫
    public GameObject Carpet1_Player4; // 플레이어 4의 첫 번째 카펫
    public GameObject Carpet2_Player4; // 플레이어 4의 두 번째 카펫

    private bool Arrangement = false; // 현재 플레이어가 카펫을 배치했는지 여부
    private bool CheckW = false;
    private bool CheckA = false;
    private bool CheckD = false;
    private bool CheckS = false;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            carpetClones[i] = new GameObject[2]; // 각 플레이어는 두 개의 카펫을 배치
        }
    }

    void Update()
    {
        // 현재 플레이어 차례인 경우만 진행
        if (currentPlayerIndex < 0 || currentPlayerIndex >= 4) return;

        Transform player = players[currentPlayerIndex];

        // 플레이어가 W 키를 눌렀을 때 카펫을 배치
        if (Input.GetKeyDown(KeyCode.W) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.W, player, new Vector3(0, 0, 1), new Vector3(0, 0, 2));
            CheckW = true;
            CheckA = false;
            CheckD = false;
            CheckS = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(0, 0, 1), new Vector3(0, 0, 2));
            CheckW = true;
            CheckA = false;
            CheckD = false;
            CheckS = false;
        }

        // W 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckW)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateCarpetPosition(player, new Vector3(0, 0, 1), new Vector3(-1, 0, 1));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(0, 0, 1), new Vector3(1, 0, 1));
            }
        }

        // A 키 눌렀을 때 처리 (W와 비슷)
        if (Input.GetKeyDown(KeyCode.A) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.A, player, new Vector3(-1, 0, 0), new Vector3(-2, 0, 0));
            CheckW = false;
            CheckA = true;
            CheckD = false;
            CheckS = false;
        }
        else if (Input.GetKeyDown(KeyCode.A) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(-1, 0, 0), new Vector3(-2, 0, 0));
            CheckW = false;
            CheckA = true;
            CheckD = false;
            CheckS = false;
        }

        // A 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckA)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateCarpetPosition(player, new Vector3(-1, 0, 0), new Vector3(-1, 0, -1));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(-1, 0, 0), new Vector3(-1, 0, 1));
            }
        }

        // D 키 눌렀을 때 처리 (W와 비슷)
        if (Input.GetKeyDown(KeyCode.D) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.D, player, new Vector3(1, 0, 0), new Vector3(2, 0, 0));
            CheckW = false;
            CheckA = false;
            CheckD = true;
            CheckS = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(1, 0, 0), new Vector3(2, 0, 0));
            CheckW = false;
            CheckA = false;
            CheckD = true;
            CheckS = false;
        }

        // D 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckD)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateCarpetPosition(player, new Vector3(1, 0, 0), new Vector3(1, 0, 1));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(1, 0, 0), new Vector3(1, 0, -1));
            }
        }
        // S 키 눌렀을 때 처리
        if (Input.GetKeyDown(KeyCode.S) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.S, player, new Vector3(0, 0, -1), new Vector3(0, 0, -2));
            CheckW = false;
            CheckA = false;
            CheckD = false;
            CheckS = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(0, 0, -1), new Vector3(0, 0, -2));
            CheckW = false;
            CheckA = false;
            CheckD = false;
            CheckS = true;
        }

        // S키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (CheckS)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateCarpetPosition(player, new Vector3(0, 0, -1), new Vector3(1, 0, -1));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(0, 0, -1), new Vector3(-1, 0, -1));
            }
        }
        // R 키로 턴을 끝내고, 카펫을 플레이어의 로컬 좌표로 수정 후, 다음 플레이어로 넘기기
        if (Input.GetKeyDown(KeyCode.R) && Arrangement)
        {
            Debug.Log("카펫 설치 완료");
            CompleteCarpetArrangement(player);
        }
    }

    // 카펫의 위치 업데이트 (로컬 좌표로 변경)
    void UpdateCarpetPosition(Transform player, Vector3 position1, Vector3 position2)
    {
        // 로컬 좌표를 월드 좌표로 변환
        Vector3 carpetPosition1 = player.TransformPoint(position1);
        Vector3 carpetPosition2 = player.TransformPoint(position2);

        // 위치가 올바르게 계산되고 있는지 확인
        Debug.Log($"Updated {currentPlayerIndex} Carpet 1 World Position: {carpetPosition1}");
        Debug.Log($"Updated {currentPlayerIndex} Carpet 2 World Position: {carpetPosition2}");

        carpetClones[currentPlayerIndex][0].transform.position = carpetPosition1;
        carpetClones[currentPlayerIndex][1].transform.position = carpetPosition2;

        SetTransparency(carpetClones[currentPlayerIndex][0], 0.5f);
        SetTransparency(carpetClones[currentPlayerIndex][1], 0.5f);
    }

    // 카펫 배치 및 클론 생성 (로컬 좌표로 변경)
    void HandleCarpetArrangement(KeyCode key, Transform player, Vector3 position1, Vector3 position2)
    {
        if (playerArrangements[currentPlayerIndex]) return;

        // 로컬 좌표를 월드 좌표로 변환
        Vector3 carpetPosition1 = player.TransformPoint(position1);
        Vector3 carpetPosition2 = player.TransformPoint(position2);

        // 각 플레이어에 맞는 카펫 프리팹을 사용하여 카펫을 배치
        switch (currentPlayerIndex)
        {
            case 0:
                carpetClones[currentPlayerIndex][0] = Instantiate(Carpet1_Player1, carpetPosition1, Quaternion.identity);
                carpetClones[currentPlayerIndex][1] = Instantiate(Carpet2_Player1, carpetPosition2, Quaternion.identity);
                break;
            case 1:
                carpetClones[currentPlayerIndex][0] = Instantiate(Carpet1_Player2, carpetPosition1, Quaternion.identity);
                carpetClones[currentPlayerIndex][1] = Instantiate(Carpet2_Player2, carpetPosition2, Quaternion.identity);
                break;
            case 2:
                carpetClones[currentPlayerIndex][0] = Instantiate(Carpet1_Player3, carpetPosition1, Quaternion.identity);
                carpetClones[currentPlayerIndex][1] = Instantiate(Carpet2_Player3, carpetPosition2, Quaternion.identity);
                break;
            case 3:
                carpetClones[currentPlayerIndex][0] = Instantiate(Carpet1_Player4, carpetPosition1, Quaternion.identity);
                carpetClones[currentPlayerIndex][1] = Instantiate(Carpet2_Player4, carpetPosition2, Quaternion.identity);
                break;
        }

        SetTransparency(carpetClones[currentPlayerIndex][0], 0.5f);
        SetTransparency(carpetClones[currentPlayerIndex][1], 0.5f);

        Arrangement = true;
        CheckW = false;
        CheckA = false;
        CheckD = false;
    }

    // 카펫 설치 완료 후 위치를 플레이어 로컬 좌표에 맞게 수정
    void CompleteCarpetArrangement(Transform player)
    {
        // 로컬 좌표를 월드 좌표로 변환하여 다시 카펫의 위치 수정
        Vector3 carpetPosition1 = player.TransformPoint(new Vector3(0, 0, 1)); // 기본 위치 예시
        Vector3 carpetPosition2 = player.TransformPoint(new Vector3(0, 0, 2)); // 기본 위치 예시

        carpetClones[currentPlayerIndex][0].transform.position = carpetPosition1;
        carpetClones[currentPlayerIndex][1].transform.position = carpetPosition2;

        SetTransparency(carpetClones[currentPlayerIndex][0], 1.0f);
        SetTransparency(carpetClones[currentPlayerIndex][1], 1.0f);

        Arrangement = false; // 카펫 배치 완료

        // 다음 플레이어로 턴을 넘김
        currentPlayerIndex = (currentPlayerIndex + 1) % 4;

        // 새 플레이어는 카펫 배치 가능
        playerArrangements[currentPlayerIndex] = false;
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
