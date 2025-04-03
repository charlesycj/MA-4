using UnityEngine;
using System.Collections;
using TMPro;

public class ErrorAlarm : MonoBehaviour
{
    public TextMeshProUGUI errorText; // TextMeshPro Text 오브젝트 연결
    public CanvasGroup canvasGroup;    // Canvas Group 컴포넌트 연결
    public float fadeDuration = 0.5f;  // 페이드 아웃 시간
    public float displayDuration = 2f; // 알림 표시 시간

    private void Start()
    {
        // 시작 시 텍스트 컴포넌트와 캔버스 그룹이 연결되었는지 확인
        if (errorText == null)
        {
            Debug.LogError("ErrorText가 연결되지 않았습니다!");
            enabled = false; // 스크립트 비활성화
        }
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup이 연결되지 않았습니다!");
            enabled = false; // 스크립트 비활성화
        }
    }

    // 에러 메시지를 표시하는 함수
    public void ShowError(string message)
    {
        errorText.text = message; // 텍스트 설정
        StartCoroutine(FadeInAndOut()); // 코루틴 시작
    }

    // 페이드 인/아웃 코루틴
    private IEnumerator FadeInAndOut()
    {
        // 페이드 인 (즉시 나타나도록 설정)
        canvasGroup.alpha = 1f;

        // 지정된 시간 동안 대기
        yield return new WaitForSeconds(displayDuration);

        // 페이드 아웃
        float time = 0f;
        float startAlpha = canvasGroup.alpha;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}