using UnityEngine;
public enum GameState
{
    play,
    gameover,
    paused
}

public class SceneController : MonoBehaviour
{

    public GameObject player2;
    public GameObject boundedMap;
    public GameObject boundlessMap;
    public GameObject GameOverScreen;
    public GameObject PauseScreen;
    public static GameState gameState;
    private void Awake()
    {
        gameState = GameState.play;
        if (GameManager.gameMode == GameModes.MultiPlayer)
        {
            player2.SetActive(true);
        }
        else player2.SetActive(false);

        if (GameManager.mapMode == MapModes.Bounded)
        {
            boundedMap.SetActive(true);
        }
        else boundedMap.SetActive(false);
    }

    private void Update()
    {
        CheckGameStatus();
    }
    private void CheckGameStatus()
    {
        if (gameState!=GameState.gameover && Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameState.play)
            {
                Time.timeScale = 0;
                PauseScreen.SetActive(true);
                gameState = GameState.paused;
            }
            else
            {
                Time.timeScale = 1;
                PauseScreen.SetActive(false);
                gameState = GameState.play;
            }
        }
        if (gameState == GameState.gameover)
        {
            GameOverScreen.SetActive(true);
        }
    }
}
