using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartandExit : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
