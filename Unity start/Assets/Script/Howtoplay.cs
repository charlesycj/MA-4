using UnityEngine;

public class Howtoplay : MonoBehaviour
{ 
    public GameObject howToPlay; 
    public void GameStart()
    {
        Time.timeScale = 0;
        howToPlay.SetActive(true);  // 활성화
    }

    public void ExitHow()
    {
        Time.timeScale = 1;
        howToPlay.SetActive(false);
    }
}
