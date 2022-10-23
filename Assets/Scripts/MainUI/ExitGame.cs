using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitGame : MonoBehaviour
{
    Button quit;
    private void Awake()
    {
        quit = this.GetComponent<Button>();
        quit.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
# if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
