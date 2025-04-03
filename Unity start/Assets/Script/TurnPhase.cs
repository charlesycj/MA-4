using UnityEngine;
using System.Linq;
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
    public int[]Score= new int[4];
    
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        for (int i = 0; i < 4; i++)
        {
            Rank[i] = -1;
        }
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
        
        
        int Islive = 0;
        for (int i = 0; i < 4; i++)
        {
            if(PlayerCheck[i]!=false)
                Islive++;
        }
       
        
        // 모든 플레이어가 차례를 마쳤다면 글로벌 턴 증가
        if (CurrentPlayerIndex >= 4 - Islive)
        {
            GlobalTurn++;
        }
        // 글로벌 턴이 3이면 게임 종료
        if (GlobalTurn == MaxGlobalTurn)
        {
            GameOver();
            Debug.Log("글로벌 턴 최대치 도달! 게임 종료!");
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
        ScoreResult();
        

        UIManager uiManager = FindObjectOfType<UIManager>();
        int winnerIndex = Rank[0] - 1; // Rank 배열은 1부터 시작하므로 인덱스 조정을 위해 -1

        string winnerName = "P" + Rank[0]; // 플레이어 이름 (예: "P1")

        int winnerScore = Score[winnerIndex]; // 플레이어 점수

        if (uiManager != null)
        {
            uiManager.SetWinnerInfo(winnerName, winnerScore);
            uiManager.EndGame(); // UIManager의 EndGame() 함수 호출
        }
        else
        {
            Debug.LogError("UIManager를 찾을 수 없습니다!");
        }
        Debug.Log("게임 종료!");
        Debug.Log($"최종 순위 1위: P{Rank[0]}  2위: P{Rank[1]}  3위: P{Rank[2]}  4위: P{Rank[3]}");
    }

    public void ScoreResult()
    {
        int[] scoreData = new int[4]; // 각 플레이어의 점수 저장
        bool[] isAlive = new bool[4]; // 살아있는 플레이어 체크

        // 점수 계산 및 파산 여부 체크
        for (int i = 0; i < 4; i++)
        {
            int totalScore = 0;

            // whosground 배열을 검사하여 해당 플레이어가 놓은 카펫 개수 계산
            for (int x = 0; x < carpetArrangement.whosground.GetLength(0); x++)
            {
                for (int z = 0; z < carpetArrangement.whosground.GetLength(1); z++)
                {
                    if (carpetArrangement.whosground[x, z] % 10 == i) // 플레이어 i가 설치한 카펫인지 확인
                    {
                        totalScore++;
                    }
                }
            }

            Score[i] = totalScore + coinCount.coin[i]; // 총 점수 계산
            scoreData[i] = Score[i]; // 점수 저장
            isAlive[i] = true; // 살아있는 플레이어 체크

            Debug.Log($"P{i + 1} 플레이어의 점수: {Score[i]}");
        }

        // 점수를 기준으로 Rank 배열 앞쪽부터 높은 순위 채우기
        for (int rankIndex = 0; rankIndex < 4; rankIndex++)
        {
            int maxIndex = -1;
            int maxScore = int.MinValue;

            // 살아있는 플레이어 중 가장 점수 높은 사람 찾기
            for (int j = 0; j < 4; j++)
            {
                if (isAlive[j] && scoreData[j] > maxScore)
                {
                    maxScore = scoreData[j];
                    maxIndex = j;
                }
            }

            // 찾은 플레이어를 Rank 배열에 추가
            if (maxIndex != -1)
            {
                Rank[rankIndex] = maxIndex + 1; // P1, P2, P3, P4 형식으로 저장
                isAlive[maxIndex] = false; // 순위가 정해진 플레이어 제외
            }
        }

       
    }
}
