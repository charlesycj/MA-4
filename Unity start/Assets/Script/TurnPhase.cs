using UnityEngine;

public enum PlayerState
{
    RotatingOrRolling, // 회전 및 주사위 던지기 단계
    PlacingCarpet,     // 카펫 배치 단계
    GameEnd
}

    
public class TurnPhase : MonoBehaviour
{
    public static TurnPhase Instance;
    public CoinCount coinCount;
    public CarpetArrangement carpetArrangement;
    
    public PlayerState CurrentState = PlayerState.RotatingOrRolling;
    public int CurrentPlayerIndex  = 0;
    public float GlobalTurn; 
    public int TotalPlayers = 4;
    public int MaxGlobalTurn=12;
    public bool[] PlayerCheck; //플레이어 파산여부 확인
    public int[]Rank= new int[4];
    public int[]Score=new int[4];
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayerCheck = new bool[TotalPlayers];  // 자동으로 false로 초기화됨
        GlobalTurn=1; 
        
        TurnUI.Instance.UpdateTurnUI(CurrentPlayerIndex);
    }

    public void SetState(PlayerState newState)
    {
        CurrentState = newState;
    }

    public void NextTurn()
    {
       
        // 모든 플레이어가 차례를 마쳤다면 글로벌 턴 증가
        if (CurrentPlayerIndex == 3)
        {
            GlobalTurn++;
        }
        // 글로벌 턴이 3이면 게임 종료
        if (GlobalTurn == 3)
        {
            GameOver();
            Debug.Log("글로벌 턴 3 도달! 게임 종료!");
            return;
        }
        
        // 생존한 플레이어 수 확인
        int alivePlayers = 0;
        int lastPlayerIndex = -1; // 마지막 생존한 플레이어 저장

        for (int i = 0; i < TotalPlayers; i++)
        {
            if (!coinCount.isBankrupt[i])
            {
                alivePlayers++;
                lastPlayerIndex = i;
            }
        }

        // 플레이어가 1명만 남으면 우승 처리 후 게임 종료
        if (alivePlayers == 1)
        {
            GameOver();
            Debug.Log($"게임 종료! 플레이어 P{lastPlayerIndex + 1} 우승");
            return;
        }

        // 현재 플레이어가 파산했다면 즉시 다음 플레이어로 넘김
        do
        {
            CurrentPlayerIndex = (CurrentPlayerIndex+1) % TotalPlayers;
        } while (coinCount.isBankrupt[CurrentPlayerIndex]); // 파산한 플레이어는 건너뜀
        
        SetState(PlayerState.RotatingOrRolling);
        Debug.Log($"플레이어 {CurrentPlayerIndex + 1}의 턴 시작!");
        
        
        TurnUI.Instance.UpdateTurnUI(CurrentPlayerIndex);
    }

    public void GameOver()
    {
        SetState(PlayerState.GameEnd);
        ResultScore();
        Debug.Log("게임 종료!");
    }
    public void ResultScore()
    {
        // 점수 배열 초기화
        for (int i = 0; i < 4; i++)
        {
            Score[i] = coinCount.coin[i]; // 코인 개수를 점수로 추가
        }

        // whosground 배열을 순회하며 각 플레이어의 카펫 개수를 점수에 추가
        for (int x = 0; x < 7; x++)
        {
            for (int z = 0; z < 7; z++)
            {
                int value = carpetArrangement.whosground[x, z];
                if (value != 0)
                {
                    int playerIndex = value % 10; // 1의 자리 값이 플레이어 인덱스
                    if (playerIndex >= 0 && playerIndex < 4)
                    {
                        Score[playerIndex] += 1; // 해당 플레이어 점수 증가
                    }
                }
            }
        }

        // 최종 점수 출력
        Debug.Log($"P1: {Score[0]}, P2: {Score[1]}, P3: {Score[2]}, P4: {Score[3]}");
    }
}
