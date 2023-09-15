using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{

    public void OnPractice()
    {
        SceneManager.LoadScene("OutdoorsScene");
    }

    public void OnButtonTwo()
    {
        SceneManager.LoadScene("PlatformSpawnScene");
    }

    public void OnExit()
    {
        Application.Quit();
        Debug.Log("quit app");
    }

    public void LoadMainScreen()
    {
        SceneManager.LoadScene("StartingScene");
    }

}
