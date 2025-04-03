using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TurnUI : MonoBehaviour
{
    public static TurnUI Instance; // 싱글턴 인스턴스
    public RectTransform[] playerPanels; // 각 플레이어 UI 패널
    public float moveXDistance = 50f; // X축 이동 거리
    public float scaleMultiplier = 1.2f; // 크기 증가 비율
    public float moveDuration = 0.5f; // 이동 시간
    
    public GameObject[] players; 

    private bool isAnimating = false; // 애니메이션 진행 중 여부
    private int  currentPlayerIndex = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 외부에서 호출할 함수 (TurnPhase에서 사용)
    public void UpdateTurnUI(int playerIndex)
    {
        if (isAnimating) return; // 애니메이션 중이면 실행하지 않음

        if (playerIndex < 0 || playerIndex >= players.Length)
        {
            Debug.LogError("Invalid player index: " + playerIndex);
        }
        
        currentPlayerIndex = playerIndex;
        ChangePlayer(playerIndex);
        StartCoroutine(AnimateTurnUI(playerPanels[playerIndex]));
        
    }

    private void ChangePlayer(int playerIndex)
    {
        foreach (GameObject player in players)
        {
            player.SetActive(false);
        }
        
        players[playerIndex].SetActive(true);
        
        Debug.Log("Changing player to: " +playerIndex);
    }

    private IEnumerator AnimateTurnUI(RectTransform panel)
    {
        isAnimating = true; // 애니메이션 진행 중

        Vector2 originalPosition = panel.anchoredPosition;
        Vector2 targetPosition = originalPosition + new Vector2(-moveXDistance, 0); // X축 이동

        Vector3 originalScale = panel.localScale;
        Vector3 targetScale = originalScale * scaleMultiplier;

        float elapsedTime = 0f;

        // UI 패널 이동 및 확대 애니메이션
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            panel.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, t);
            panel.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = targetPosition;
        panel.localScale = targetScale;

        yield return new WaitForSeconds(0.5f); // 유지 시간

        elapsedTime = 0f;

        // UI 패널 원래 크기 및 위치로 되돌리기
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            panel.anchoredPosition = Vector2.Lerp(targetPosition, originalPosition, t);
            panel.localScale = Vector3.Lerp(targetScale, originalScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        panel.anchoredPosition = originalPosition;
        panel.localScale = originalScale;

        isAnimating = false; // 애니메이션 종료
    }
}
