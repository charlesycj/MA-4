using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public CoinCount coinCount; // Inspector에서 연결
    public TextMeshProUGUI[] coinTexts; // 플레이어별 코인 UI Text 연결
   
    
    void Update()
    {
        UpdateCoinUI();
        
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


}