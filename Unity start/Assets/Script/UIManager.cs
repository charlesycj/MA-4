using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CoinCount coinCount; // Inspector에서 연결
    public TextMeshProUGUI[] coinTexts; // 플레이어별 코인 UI Text 연결
    public TurnPhase turnPhase;
    public TextMeshProUGUI[] rankTexts;
    
    private int[] playerRanks = new int[4]; 
    
    //우승자 정보 저장
    private string winnerName;
    private int winnerScore;
    void Update()
    {
        UpdateCoinUI();
        UpdateTurnUI();
        
    }
    
    void UpdateCoinUI()
    {
        if (coinCount != null && coinTexts.Length == coinCount.coin.Length)
        {
            for (int i = 0; i < coinCount.coin.Length; i++)
            {
                coinTexts[i].text = coinCount.coin[i].ToString();
            }
        }
    }

    void UpdateTurnUI()
    {
        if (turnPhase == null || rankTexts == null || rankTexts.Length != 4)
        {
            Debug.LogError("TurnPhase 또는 playerRankTexts가 null이거나 배열 길이가 일치하지 않습니다.");
            return;
        }

        // 각 플레이어의 순위를 저장할 배열 (인덱스: 플레이어 번호 - 1, 값: 순위)
        int[] playerRanks = new int[4];

        // TurnPhase의 Rank 배열을 순회하면서 각 플레이어의 순위를 계산
        for (int i = 0; i < 4; i++)
        {
            int playerNumber = turnPhase.Rank[i]; // Rank 배열에는 플레이어 "번호"가 저장됨 (1부터 시작)

            // 플레이어 번호가 유효한 범위 내에 있는지 확인
            if (playerNumber >= 1 && playerNumber <= 4)
            {
                playerRanks[playerNumber - 1] = i + 1; // 순위 저장 (1부터 시작)
            }
            else
            {
               // Debug.LogError($"TurnPhase.Rank[{i}]의 값이 잘못되었습니다: {playerNumber}");
            }
        }

        // 각 플레이어의 순위를 UI 텍스트에 출력
        for (int i = 0; i < 4; i++)
        {
            rankTexts[i].text = playerRanks[i].ToString();
        }
    }
    //우승자 정보 저장 함수
    public void SetWinnerInfo(string Name, int Score)
    {
        winnerName = Name;
        winnerScore = Score;
    }
    //씬 전환
    public void EndGame()
    {
        PlayerPrefs.SetString("winnerName", winnerName);
        PlayerPrefs.SetInt("winnerScore", winnerScore);
        PlayerPrefs.Save();
        
        SceneManager.LoadScene("Scenes/EndScence");
        
    }
}


