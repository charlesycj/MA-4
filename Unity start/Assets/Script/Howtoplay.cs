using UnityEngine;

public class Howtoplay : MonoBehaviour
{ 
    public GameObject howToPlay; 
    public void GameStart()
    {
        howToPlay.SetActive(true);  // 활성화
    }

    public void ExitHow()
    {
        howToPlay.SetActive(false);
    }
}
