using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartScence : MonoBehaviour
{
    public Button ExitButton;
    public Button StartButton;
    public GameObject howToPlay; 
   

    public void Gameexite()
    {
        Application.Quit();
        Debug.Log("Gameexite");
    }

    public void GameStart()
    {
        
        howToPlay.SetActive(true);  // 활성화
    }
}
