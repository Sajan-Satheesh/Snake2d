using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HelpGame : MonoBehaviour
{
    private Button help;
    [SerializeField] Button back;
    [SerializeField] GameObject helpPanel;
    private void Awake()
    {
        BackToGame();
        help = GetComponent<Button>();
        help.onClick.AddListener(HelpsGame);
        back.onClick.AddListener(BackToGame);
    }

    private void BackToGame()
    {
        helpPanel.SetActive(false);
    }

    private void HelpsGame()
    {
        helpPanel.SetActive(true);
    }
}
