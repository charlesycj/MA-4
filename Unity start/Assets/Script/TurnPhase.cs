using UnityEngine;

public enum PlayerState
{
    RotatingOrRolling, // 회전 및 주사위 던지기 단계
    PlacingCarpet,     // 카펫 배치 단계
    TurnEnded          // 턴 종료 후 대기 상태
}
public class TurnPhase : MonoBehaviour
{
    public static TurnPhase Instance;

    public PlayerState CurrentState { get; private set; } = PlayerState.RotatingOrRolling;
    public int CurrentPlayerIndex { get; private set; } = 0;
    public int TotalPlayers = 4;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetState(PlayerState newState)
    {
        CurrentState = newState;
    }

    public void NextTurn()
    {
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % TotalPlayers;
        SetState(PlayerState.RotatingOrRolling);
        Debug.Log($"플레이어 {CurrentPlayerIndex + 1}의 턴 시작!");
    }
}
