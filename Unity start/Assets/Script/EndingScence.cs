using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EndingScence : MonoBehaviour
{
    public PlayableDirector TimeLine ;
    public GameObject uiObject;
    public float uiActivationTime = 5f; // UI를 활성화할 시간
    public TextMeshProUGUI winnerNameText;
    public TextMeshProUGUI winnerScoreText;

    void Start()
    {
        string winnerName = PlayerPrefs.GetString("WinnerName",PlayerPrefs.GetString("winnerName"));
        int winnerScore = PlayerPrefs.GetInt("WinnerScore", PlayerPrefs.GetInt("winnerScore"));

        if (winnerName == null || winnerScore == null)
        {
            winnerName = "Unknown";
            winnerScore = 0;
        }
            
        winnerNameText.text =  winnerName;
        winnerScoreText.text =  winnerScore.ToString();
    }
    void Update()
    {
        if (TimeLine.time >= uiActivationTime && !uiObject.activeSelf)
        {
            uiObject.SetActive(true);
        }
        
    }
}
