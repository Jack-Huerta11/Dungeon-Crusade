using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Color backgroundColor = Color.black;

    void Start()
    {
        Camera.main.backgroundColor = backgroundColor;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
