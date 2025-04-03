using UnityEngine;

public class CarpetArrangement : MonoBehaviour
{
    public Transform[] players; // 4명의 플레이어
    public TurnPhase turnPhase;
    public bool[] playerArrangements = new bool[4]; // 각 플레이어가 카펫을 배치했는지 여부
    public TurnUI turnUI;
    public GameObject[][] carpetClones = new GameObject[4][]; // 각 플레이어의 카펫 클론
    public ErrorAlarm errorAlarm;
    
    // 삭제용 태그 목록
    string[] carpetTags = { "CarpetP1", "CarpetP2", "CarpetP3", "CarpetP4" };
    
    public int[,] whosground = new int[7, 7]; // 정적 2D 배열
   
    //카펫 프리팹 할당
    public GameObject Carpet1_Player1; public GameObject Carpet2_Player1;
    public GameObject Carpet1_Player2; public GameObject Carpet2_Player2;
    public GameObject Carpet1_Player3; public GameObject Carpet2_Player3;
    public GameObject Carpet1_Player4; public GameObject Carpet2_Player4;

   
    
    private bool Arrangement = false; // 현재 플레이어가 카펫을 배치했는지 여부

    private  enum Direction //플레이어의 키 추적
    {
        CheckW,
            CheckA,
        CheckD ,
            CheckS,
            Null
    }
    
    Direction PlayerDirection=Direction.Null;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            carpetClones[i] = new GameObject[2]; // 각 플레이어는 두 개의 카펫을 배치
        }
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                whosground[x, y] = -1;
            }
        }
    }

    public void Update()
    {
        if (TurnPhase.Instance.CurrentState != PlayerState.GameEnd && turnPhase.PlayerCheck[turnPhase.CurrentPlayerIndex]!=false)
        {
            turnPhase.SetState(PlayerState.RotatingOrRolling);
            turnPhase.CurrentPlayerIndex+=1;
          
        }
        if (TurnPhase.Instance.CurrentState != PlayerState.PlacingCarpet) return;
        // 현재 플레이어 차례인 경우만 진행
        if (turnPhase.CurrentPlayerIndex < 0 || turnPhase.CurrentPlayerIndex >= 4) return;

        Transform player = players[turnPhase.CurrentPlayerIndex];

        //w/a/s/d를 누르지 않고 q/e를 누른 경우
        
             if (Input.GetKeyDown(KeyCode.Q) && PlayerDirection==Direction.Null)
            {
                Debug.Log("W/A/S/D중 하나를 눌러 방향을 먼저 지정해주세요!");
            }
            if (Input.GetKeyDown(KeyCode.E) && PlayerDirection==Direction.Null)
            {
                Debug.Log("W/A/S/D중 하나를 눌러 방향을 먼저 지정해주세요!");
            }
        
        // 플레이어가 W 키를 눌렀을 때 카펫을 배치
        if (Input.GetKeyDown(KeyCode.W) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.W, player, new Vector3(0, 0.3f, 1), new Vector3(0, 0.3f, 2));
            PlayerDirection = Direction.CheckW;
        }
        else if (Input.GetKeyDown(KeyCode.W) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(0, 0.3f, 1), new Vector3(0, 0.3f, 2));
            PlayerDirection = Direction.CheckW;
        }
        
      
        // W 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (PlayerDirection==Direction.CheckW)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateCarpetPosition(player, new Vector3(0, 0.3f, 1), new Vector3(-1, 0.3f, 1));
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(0, 0.3f, 1), new Vector3(1, 0.3f, 1));
            }
        }

        // A 키 눌렀을 때 처리 
        if (Input.GetKeyDown(KeyCode.A) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.A, player, new Vector3(-1, 0.3f, 0), new Vector3(-2, 0.3f, 0));
            PlayerDirection = Direction.CheckA;
        }
        else if (Input.GetKeyDown(KeyCode.A) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(-1, 0.3f, 0), new Vector3(-2, 0.3f, 0));
            PlayerDirection = Direction.CheckA;
        }

        // A 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if (PlayerDirection == Direction.CheckA)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateCarpetPosition(player, new Vector3(-1,0.3f, 0), new Vector3(-1, 0.3f, -1));
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(-1, 0.3f, 0), new Vector3(-1, 0.3f, 1));
            }
        }

        // D 키 눌렀을 때 처리
        if (Input.GetKeyDown(KeyCode.D) && !Arrangement)
        {
            HandleCarpetArrangement(KeyCode.D, player, new Vector3(1, 0.3f, 0), new Vector3(2,0.3f, 0));
            PlayerDirection = Direction.CheckD;
        }
        else if (Input.GetKeyDown(KeyCode.D) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(1, 0.3f, 0), new Vector3(2,0.3f, 0));
            PlayerDirection = Direction.CheckD;
        }

        // D 키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if ( PlayerDirection == Direction.CheckD)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateCarpetPosition(player, new Vector3(1,0.3f, 0), new Vector3(1,0.3f, 1));
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
            PlayerDirection = Direction.CheckS;
        }
        else if (Input.GetKeyDown(KeyCode.S) && Arrangement)
        {
            UpdateCarpetPosition(player, new Vector3(0, 0.3f, -1), new Vector3(0, 0.3f, -2));
            PlayerDirection = Direction.CheckS;
        }

        // S키를 눌러 카펫이 배치된 이후에만 Q, E 키 입력 가능
        if ( PlayerDirection == Direction.CheckS)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateCarpetPosition(player, new Vector3(0, 0.3f, -1), new Vector3(1, 0.3f, -1));
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateCarpetPosition(player, new Vector3(0,0.3f, -1), new Vector3(-1, 0.3f, -1));
            }
        }
        // R 키로 턴을 끝내고, 카펫을 플레이어의 로컬 좌표로 수정 후, 다음 플레이어로 넘기기
        if (Input.GetKeyDown(KeyCode.R) && Arrangement)
        {
            CompleteCarpetArrangement(player);
            PlayerDirection = Direction.Null;
         
        }
    }

    // 카펫의 위치 업데이트 (로컬 좌표로 변경)
    public void UpdateCarpetPosition(Transform player, Vector3 position1, Vector3 position2)
    {
        // 로컬 좌표를 월드 좌표로 변환r
        Vector3 carpetPosition1 = player.TransformPoint(position1);
        Vector3 carpetPosition2 = player.TransformPoint(position2);
        
        carpetClones[turnPhase.CurrentPlayerIndex][0].transform.position = carpetPosition1;
        carpetClones[turnPhase.CurrentPlayerIndex][1].transform.position = carpetPosition2;

        SetTransparency(carpetClones[turnPhase.CurrentPlayerIndex][0], 0.5f);
        SetTransparency(carpetClones[turnPhase.CurrentPlayerIndex][1], 0.5f);
    }

    // 카펫 배치 및 클론 생성 (로컬 좌표로 변경)
    public void HandleCarpetArrangement(KeyCode key, Transform player, Vector3 position1, Vector3 position2)
    {
        if (playerArrangements[turnPhase.CurrentPlayerIndex]) return;

        // 로컬 좌표를 월드 좌표로 변환
        Vector3 carpetPosition1 = player.TransformPoint(position1);
        Vector3 carpetPosition2 = player.TransformPoint(position2);

        // 각 플레이어에 맞는 카펫 프리팹을 사용하여 카펫을 배치
        switch (turnPhase.CurrentPlayerIndex)
        {
            case 0:
                carpetClones[turnPhase.CurrentPlayerIndex][0] = Instantiate(Carpet1_Player1, carpetPosition1, Quaternion.identity);
                carpetClones[turnPhase.CurrentPlayerIndex][1] = Instantiate(Carpet2_Player1, carpetPosition2, Quaternion.identity);
                break;
            case 1:
                carpetClones[turnPhase.CurrentPlayerIndex][0] = Instantiate(Carpet1_Player2, carpetPosition1, Quaternion.identity);
                carpetClones[turnPhase.CurrentPlayerIndex][1] = Instantiate(Carpet2_Player2, carpetPosition2, Quaternion.identity);
                break;
            case 2:
                carpetClones[turnPhase.CurrentPlayerIndex][0] = Instantiate(Carpet1_Player3, carpetPosition1, Quaternion.identity);
                carpetClones[turnPhase.CurrentPlayerIndex][1] = Instantiate(Carpet2_Player3, carpetPosition2, Quaternion.identity);
                break;
            case 3:
                carpetClones[turnPhase.CurrentPlayerIndex][0] = Instantiate(Carpet1_Player4, carpetPosition1, Quaternion.identity);
                carpetClones[turnPhase.CurrentPlayerIndex][1] = Instantiate(Carpet2_Player4, carpetPosition2, Quaternion.identity);
                break;
        }

        SetTransparency(carpetClones[turnPhase.CurrentPlayerIndex][0], 0.5f);
        SetTransparency(carpetClones[turnPhase.CurrentPlayerIndex][1], 0.5f);

        Arrangement = true;
      
        
    }
    
    // 카펫 설치 완료 후 위치를 플레이어 로컬 좌표에 맞게 수정
   public void CompleteCarpetArrangement(Transform player)
    {
        // 카펫의 월드 좌표를 얻어옴
        Vector3 pos0 = carpetClones[turnPhase.CurrentPlayerIndex][0].transform.position;
        Vector3 pos1 = carpetClones[turnPhase.CurrentPlayerIndex][1].transform.position;

        // 카펫 좌표에서 x, z 값 추출 후, 정수로 반올림하여 배열 인덱스로 사용 -3이면:0, 0이면:3, 3이면 6
        int x0 = Mathf.RoundToInt(pos0.x + 3); 
        int z0 = Mathf.RoundToInt(pos0.z + 3);  
        int x1 = Mathf.RoundToInt(pos1.x + 3);
        int z1 = Mathf.RoundToInt(pos1.z + 3);
        
        // 설치 가능한 범위는 예시로 (0,7) 내부라고 가정 (즉, 1~6만 허용)
 
         if ((x0 < 0 || x0 >= 7 || z0 < 0 || z0 >= 7) || (x1 < 0 || x1 >= 7 || z1 < 0 || z1 >= 7))
         {
             errorAlarm.ShowError("Carpet cannot be installed out of range.");
            Debug.Log("카펫이 설치 범위를 벗어났습니다. 설치할 수 없습니다.");
            return;
        }
        // 이미 카펫이 설치되어 있다면 (두 좌표 모두 0이 아닌 값이며, 값이 같으면) 설치를 취소
        if (whosground[x0, z0] != -1 && whosground[x1, z1] != -1 && whosground[x0, z0] == whosground[x1, z1])
        {
            errorAlarm.ShowError("It overlaps exactly with the existing carpet! Please install it somewhere else!");
            Debug.Log("기존에 설치된 카펫에 정확히 겹칩니다! 다른 곳에 설치해주세요!");
            return;
        }
        
        // 좌표 x0, z0에 해당하는 셀에 있는 카펫 삭제 (설치하려는 카펫 제외)
        if (whosground[x0, z0] != -1)
        {
            foreach (string tag in carpetTags)
            {
                GameObject[] carpets = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject carpet in carpets)
                {
                    // 현재 설치 중인 카펫이라면 삭제하지 않음
                    if (carpet == carpetClones[turnPhase.CurrentPlayerIndex][0] || carpet == carpetClones[turnPhase.CurrentPlayerIndex][1])
                        continue;
            
                    Vector3 pos = carpet.transform.position;
                    int gridX = Mathf.RoundToInt(pos.x + 3);
                    int gridZ = Mathf.RoundToInt(pos.z + 3);
                    if (gridX == x0 && gridZ == z0)
                    {
                        Destroy(carpet);
                    }
                }
            }
            whosground[x0, z0] = -1;
        }

// 좌표 x1, z1에 해당하는 셀에 있는 카펫 삭제 (설치하려는 카펫 제외)
        if (whosground[x1, z1] != -1)
        {
            foreach (string tag in carpetTags)
            {
                GameObject[] carpets = GameObject.FindGameObjectsWithTag(tag);
                foreach (GameObject carpet in carpets)
                {
                    // 현재 설치 중인 카펫이라면 삭제하지 않음
                    if (carpet == carpetClones[turnPhase.CurrentPlayerIndex][0] || carpet == carpetClones[turnPhase.CurrentPlayerIndex][1])
                        continue;
            
                    Vector3 pos = carpet.transform.position;
                    int gridX = Mathf.RoundToInt(pos.x + 3);
                    int gridZ = Mathf.RoundToInt(pos.z + 3);
                    if (gridX == x1 && gridZ == z1)
                    {
                        Destroy(carpet);
                    }
                }
            }
            whosground[x1, z1] = -1;
        }
        
        SetTransparency(carpetClones[turnPhase.CurrentPlayerIndex][0], 1.0f);
        SetTransparency(carpetClones[turnPhase.CurrentPlayerIndex][1], 1.0f);
        
        // 해당 위치가 누구의 땅이고 몇턴에 설치했는지 기록
        int playerMark = (Mathf.FloorToInt(turnPhase.GlobalTurn) * 10) + turnPhase.CurrentPlayerIndex; //10의 자리 글로벌 턴 1의자리 플레이어 구분
        whosground[x0, z0] = playerMark;
        whosground[x1, z1] = playerMark;
        
        // 카펫의 y 좌표를 0.1f로 수정 (x, z 값은 그대로 유지)
        carpetClones[turnPhase.CurrentPlayerIndex][0].transform.position = new Vector3(pos0.x, 0.1f, pos0.z);
        carpetClones[turnPhase.CurrentPlayerIndex][1].transform.position = new Vector3(pos1.x, 0.1f, pos1.z);

        Arrangement = false; // 카펫 배치 완료
        
        Debug.Log($"해당 구역 (x좌표:{x0-3}, z좌표:{z0-3}) 와 (x좌표:{x1-3}, z좌표:{z1-3})를 " + 
                  $"플레이어{turnPhase.CurrentPlayerIndex+1}의 땅으로 변경 ");
        Debug.Log($"배열 {x0},{z0}와  {x1},{z1}를 {whosground[x0,z0]+1}로 변경");
        
        TurnPhase.Instance.NextTurn();
        // 점수 갱신
        turnPhase.ScoreResult();
        Debug.Log($"현재 순위 1위: P{turnPhase.Rank[0]}  2위: P{turnPhase.Rank[1]}  3위: P{turnPhase.Rank[2]}  4위: P{turnPhase.Rank[3]}");
        // 새 플레이어는 카펫 배치 가능
        playerArrangements[turnPhase.CurrentPlayerIndex] = false;
    }
   
    // 투명도를 설정하는 함수
    public void SetTransparency(GameObject carpet, float alphaValue)
    {
        if (carpet == null) return;

        Renderer carpetRenderer = carpet.GetComponent<Renderer>();
        if (carpetRenderer == null) return;

        Color color = carpetRenderer.material.color;
        color.a = alphaValue;
        carpetRenderer.material.color = color;
    }
    public void RemoveAllCarpetsOfPlayer(int playerIndex)
    {
        string targetTag = "CarpetP" + (playerIndex + 1);
        GameObject[] carpets = GameObject.FindGameObjectsWithTag(targetTag);

        Debug.Log($"[RemoveAllCarpetsOfPlayer] Removing all carpets with tag: {targetTag}, Found: {carpets.Length}");

        foreach (GameObject carpet in carpets)
        {
            Debug.Log($"Destroying: {carpet.name} with tag {carpet.tag}");
            Destroy(carpet);
        }

        // whosground 배열 초기화 (해당 플레이어의 흔적을 지움)
        for (int x = 0; x < 7; x++)
        {
            for (int z = 0; z < 7; z++)
            {
                if (whosground[x, z] % 10 == (playerIndex)) // 해당 플레이어가 깔았던 위치인지 확인
                {
                    whosground[x, z] = -1;
                    Debug.Log($"Cleared whosground[{x}, {z}]");
                }
            }
        }
    } 
    
}