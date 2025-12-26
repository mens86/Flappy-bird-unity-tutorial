using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.PlayMusic("Menu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        AudioManager.instance.PlayMusic("Level");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
