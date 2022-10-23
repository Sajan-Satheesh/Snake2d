using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayGame : MonoBehaviour
{
    private Button play;
    private void Awake()
    {
        play = GetComponent<Button>();
        play.onClick.AddListener(PlaysGame);
    }

    private void PlaysGame()
    {
        SceneManager.LoadScene(1);
    }
}
