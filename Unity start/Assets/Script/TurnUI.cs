using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour
{
    public RectTransform[] playerPanels; // 플레이어 UI 패널
    public float moveDistance = 50f; // Y축 이동 거리
    public float scaleMultiplier = 1.2f; // 크기 증가 비율
    public float moveDuration = 0.5f; // 이동 시간
    

    private int currentPlayerIndex = 0; // 현재 플레이어 인덱스
    private bool isAnimating = false; // 애니메이션 진행 중 여부

    public CarpetArrangement carpetArrangement; // CarpetArrangement 스크립트 연결

    private void Start()
    {
        UpdateTurn(currentPlayerIndex);
    }

    public void NextTurn()
    {
        if (isAnimating) return; // 애니메이션 중이면 턴 진행하지 않음
        currentPlayerIndex = (currentPlayerIndex + 1) % playerPanels.Length;
        UpdateTurn(currentPlayerIndex);
    }

    private void UpdateTurn(int playerIndex)
    {
        isAnimating = true; // 애니메이션 시작
        StartCoroutine(MoveAndScale(playerPanels[playerIndex]));
    }

    private IEnumerator MoveAndScale(RectTransform panel)
    {
        

        Vector2 originalPosition = panel.anchoredPosition;
        Vector2 targetPosition = originalPosition + new Vector2(-moveDistance, 0); // X축 이동

        Vector3 originalScale = panel.localScale;
        Vector3 targetScale = originalScale * scaleMultiplier;

        float elapsedTime = 0f;

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

        yield return new WaitForSeconds(1f);

        elapsedTime = 0f;
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
