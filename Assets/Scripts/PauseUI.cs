using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseUIscreen;
    public Button resume;
    public Button mainMenu;

    private void Awake()
    {
        resume.onClick.AddListener(ResumeGame);
        mainMenu.onClick.AddListener(GetMainMenu);
    }


    private void GetMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void ResumeGame()
    {
        pauseUIscreen.SetActive(false);
        SceneController.gameState = GameState.play;
        Time.timeScale=1;
    }
}
