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

    public PlayerState CurrentState = PlayerState.RotatingOrRolling;
    public int CurrentPlayerIndex  = 0;
    public int TotalPlayers = 4;
    public bool[] PlayerCheck; //플레이어 파산여부 확인

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
    }

    public void SetState(PlayerState newState)
    {
        CurrentState = newState;
    }

    public void NextTurn()
    {
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
    }

    public void GameOver()
    {
        
    }
}
