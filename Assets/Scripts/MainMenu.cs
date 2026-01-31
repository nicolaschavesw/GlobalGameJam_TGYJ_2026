using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelWork");
        // o por índice:
        // SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        Debug.Log("Opciones");
    }

    public void OpenCredits()
    {
        Debug.Log("Créditos");
    }

    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
