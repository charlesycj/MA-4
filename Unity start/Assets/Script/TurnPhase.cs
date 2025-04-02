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
        Debug.Log("게임 종료!");

        int remainingPlayers = 0;

        // 1. 아직 순위가 매겨지지 않은 플레이어 수 계산
        foreach (int rank in Rank)
        {
            if (rank == 0) remainingPlayers++;
        }

        // 2. CoinCount의 coin 배열을 기반으로 순위 결정
        if (remainingPlayers > 1) // 아직 순위가 정해지지 않은 플레이어가 2명 이상이라면
        {
            CoinCount coinCount = FindObjectOfType<CoinCount>(); // CoinCount 인스턴스 찾기
            if (coinCount == null)
            {
                Debug.LogError("CoinCount 객체를 찾을 수 없습니다!");
                return;
            }

            int[] coins = coinCount.coin; // CoinCount의 coin 배열 직접 참조
            int[] sortedIndexes = new int[remainingPlayers]; // 정렬할 플레이어 인덱스 배열
            int index = 0;

            // 3. 아직 순위가 정해지지 않은 플레이어들의 인덱스를 배열에 저장
            for (int i = 0; i < Rank.Length; i++)
            {
                if (Rank[i] == 0)
                {
                    sortedIndexes[index] = i;
                    index++;
                }
            }

            // 4. 버블 정렬을 사용해 코인 개수를 기준으로 내림차순 정렬
            for (int i = 0; i < remainingPlayers - 1; i++)
            {
                for (int j = 0; j < remainingPlayers - 1 - i; j++)
                {
                    if (coins[sortedIndexes[j]] < coins[sortedIndexes[j + 1]])
                    {
                        // 두 값을 스왑
                        int temp = sortedIndexes[j];
                        sortedIndexes[j] = sortedIndexes[j + 1];
                        sortedIndexes[j + 1] = temp;
                    }
                }
            }

            // 5. 정렬된 순서대로 높은 등수부터 배정 (1등부터 채우는 게 아니라, 가장 높은 Rank부터 채우기)
            int rankValue = Rank.Length - remainingPlayers + 1; // 1등이 아닌, Rank 배열의 남은 부분부터 채움
            for (int i = 0; i < remainingPlayers; i++)
            {
                Rank[sortedIndexes[i]] = rankValue;
                rankValue++;
            }
        }
    }
}
