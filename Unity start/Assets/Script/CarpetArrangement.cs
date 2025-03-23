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
    
    //플레이어가 지정한 방향 확인 (이후 Q나 E 누를시 상호작용 확인)
    private bool CheckW = false;
    private bool CheckA = false;
    private bool CheckD = false;
    private bool CheckS = false;
    
    
    RaycastHit hit; //카펫 설치시 해당 좌표에 다른 카펫이 있나 검토를 위한 변수

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
            HandleCarpetArrangement(KeyCode.W, player, new Vector3(0, 0.3f, 1), new Vector3(0, 0.3f, 2f));
            CheckW = true;
            CheckA = false;
            CheckD = false;
            CheckS = false;
        }
        else if (Input.GetKeyDown(KeyCode.W) && Arrangement)
        {
            HandleCarpetArrangement(KeyCode.W, player, new Vector3(0, 0.3f, 1f), new Vector3(0, 0.3f, 2f));
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
                UpdateCarpetPosition(player, new Vector3(0, 0.3f, 1), new Vector3(-1, 0.3f, 1));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(0,0.3f, 1), new Vector3(1,0.3f, 1));
            }
        }

        // A 키 눌렀을 때 처리 (W와 비슷)
        if (Input.GetKeyDown(KeyCode.A) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.A, player, new Vector3(-1, 0.3f, 0), new Vector3(-2, 0.3f, 0));
            CheckW = false;
            CheckA = true;
            CheckD = false;
            CheckS = false;
        }
        else if (Input.GetKeyDown(KeyCode.A) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(-1, 0.3f, 0), new Vector3(-2, 0.3f, 0));
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
                UpdateCarpetPosition(player, new Vector3(-1, 0.3f, 0), new Vector3(-1, 0.3f, -1));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(-1, 0.3f, 0), new Vector3(-1, 0.3f, 1));
            }
        }

        // D 키 눌렀을 때 처리 (W와 비슷)
        if (Input.GetKeyDown(KeyCode.D) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.D, player, new Vector3(1, 0.3f, 0), new Vector3(2, 0.3f, 0));
            CheckW = false;
            CheckA = false;
            CheckD = true;
            CheckS = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(1, 0.3f, 0), new Vector3(2, 0.3f, 0));
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
                UpdateCarpetPosition(player, new Vector3(1, 0.3f, 0), new Vector3(1, 0.3f, 1));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(1, 0.3f, 0), new Vector3(1, 0.3f, -1));
            }
        }
        // S 키 눌렀을 때 처리
        if (Input.GetKeyDown(KeyCode.S) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.S, player, new Vector3(0, 0.3f, -1), new Vector3(0, 0.3f, -2));
            CheckW = false;
            CheckA = false;
            CheckD = false;
            CheckS = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(0, 0.3f, -1), new Vector3(0, 0.3f, -2));
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
                UpdateCarpetPosition(player, new Vector3(0, 0.3f, -1f), new Vector3(1, 0.3f, -1f));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(0, 0.3f, -1f), new Vector3(-1, 0.3f, -1f));
            }
        }
        // R 키로 턴을 끝내고, 카펫을 플레이어의 로컬 좌표로 수정 후, 다음 플레이어로 넘기기
        if (Input.GetKeyDown(KeyCode.R) && Arrangement)
        {
           
            CompleteCarpetArrangement(player);
        }
    }

    // 카펫의 위치 업데이트 (로컬 좌표로 변경)
    void UpdateCarpetPosition(Transform player, Vector3 position1, Vector3 position2)
    {
        // 만약 carpetClones가 생성되지 않았다면 HandleCarpetArrangement 먼저 실행
        if (carpetClones[currentPlayerIndex][0] == null || carpetClones[currentPlayerIndex][1] == null)
        {
            HandleCarpetArrangement(KeyCode.None, player, position1, position2);
        }

        Vector3 carpetPosition1 = player.TransformPoint(position1);
        Vector3 carpetPosition2 = player.TransformPoint(position2);

        carpetClones[currentPlayerIndex][0].transform.position = carpetPosition1;
        carpetClones[currentPlayerIndex][1].transform.position = carpetPosition2;

        SetTransparency(carpetClones[currentPlayerIndex][0], 0.5f);
        SetTransparency(carpetClones[currentPlayerIndex][1], 0.5f);
    }

    // 카펫 청사진 생성 (로컬 좌표로 변경)
    void HandleCarpetArrangement(KeyCode key, Transform player, Vector3 position1, Vector3 position2)
    {
        if (playerArrangements[currentPlayerIndex] || Arrangement) return; // 이미 배치했으면 종료

        Vector3 carpetPosition1 = player.TransformPoint(position1);
        Vector3 carpetPosition2 = player.TransformPoint(position2);

        // 이미 청사진 카펫이 있다면 위치만 업데이트
        if (carpetClones[currentPlayerIndex][0] != null && carpetClones[currentPlayerIndex][1] != null)
        {
            carpetClones[currentPlayerIndex][0].transform.position = carpetPosition1;
            carpetClones[currentPlayerIndex][1].transform.position = carpetPosition2;
        }
        else
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

        Arrangement = true; // 한 턴에 한 번만 배치 가능하도록 설정
    }

    // 카펫 설치 완료 후 위치를 플레이어 로컬 좌표에 맞게 수정
    void CompleteCarpetArrangement(Transform player)
    {
        Vector3 pos1 = carpetClones[currentPlayerIndex][0].transform.position;
        Vector3 pos2 = carpetClones[currentPlayerIndex][1].transform.position;
        
        // 각각의 칸이 이미 불투명한(배치된) 카펫과 겹치는지 확인
        bool carpet1Blocked = IsCarpetAlreadyPlaced(pos1);
        bool carpet2Blocked = IsCarpetAlreadyPlaced(pos2);

        // **두 칸 모두 기존 불투명한 카펫과 겹칠 때만 설치 불가**
        if (carpet1Blocked && carpet2Blocked)
        {
            Debug.Log("두 개의 카펫이 모두 기존 카펫과 겹쳐서 설치할 수 없습니다!");
            return;
        }

        // 기존 청사진 카펫 제거 후 배치 완료
        RemoveExistingCarpets(pos1);
        RemoveExistingCarpets(pos2);

        // 카펫 1과 2의 y 좌표를 0.1f로 수정
        pos1.y = 0.1f;
        pos2.y = 0.1f;

        // 새로운 위치로 카펫을 배치
        carpetClones[currentPlayerIndex][0].transform.position = pos1;
        carpetClones[currentPlayerIndex][1].transform.position = pos2;
        
        //불투명하게 변경(설치완료)
        SetTransparency(carpetClones[currentPlayerIndex][0], 1.0f);
        SetTransparency(carpetClones[currentPlayerIndex][1], 1.0f);
        
        
        Arrangement = false;
        currentPlayerIndex = (currentPlayerIndex + 1) % 4;
        playerArrangements[currentPlayerIndex] = false;
    }
    
    // 해당 위치에 불투명한(완전히 설치된) 카펫이 있는지 확인
    bool IsCarpetAlreadyPlaced(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapBox(position, new Vector3(0.45f, 0.1f, 0.45f));
        foreach (Collider hitCollider in hitColliders)
        {
            // 카펫 태그 확인
            if (hitCollider.CompareTag("CarpetP1") ||
                hitCollider.CompareTag("CarpetP2") ||
                hitCollider.CompareTag("CarpetP3") ||
                hitCollider.CompareTag("CarpetP4"))
            {
                // Renderer에서 alpha 값 확인
                Renderer renderer = hitCollider.GetComponent<Renderer>();
                if (renderer != null && renderer.material.HasProperty("_Color"))
                {
                    Color color = renderer.material.color;
                    if (color.a == 1.0f) // 완전히 불투명한 경우
                    {
                        return true; // 설치 불가
                    }
                }
            }
        }
        return false; // 설치 가능
    }
    //만약 카펫이 배치되어있다면 삭제
    void RemoveExistingCarpets(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapBox(position, new Vector3(0.5f, 0.5f, 0.5f));
        foreach (Collider hitCollider in hitColliders)
        {
            // 태그가 CarpetP1, CarpetP2, CarpetP3, CarpetP4 중 하나인지 확인
            if (!(hitCollider.CompareTag("CarpetP1") || 
                  hitCollider.CompareTag("CarpetP2") || 
                  hitCollider.CompareTag("CarpetP3") || 
                  hitCollider.CompareTag("CarpetP4")))
            {
                continue;
            }

            Renderer carpetRenderer = hitCollider.GetComponent<Renderer>();
            if (carpetRenderer == null) continue; // Renderer가 없으면 건너뛰기

            Color carpetColor = carpetRenderer.material.color;

            // alpha 값이 1.0인 경우(완전히 불투명한 경우)만 제거
            if (Mathf.Approximately(carpetColor.a, 1.0f))
            {
                Bounds carpetBounds = hitCollider.bounds;
                Bounds targetBounds = new Bounds(position, new Vector3(1.0f, 0.1f, 1.0f)); // 현재 위치 기준 카펫 영역 설정

                // 영역이 겹칠 때만 제거
                if (carpetBounds.Intersects(targetBounds))
                {
                    Destroy(hitCollider.gameObject);
                }
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