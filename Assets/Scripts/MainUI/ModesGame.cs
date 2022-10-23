
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ModesGame : MonoBehaviour
{
    Button Modes;
    [SerializeField] Button Back;
    public GameObject modeWindow;
    public Toggle MultiplayerOption;
    public Toggle BoundedOption;
    private void Awake()
    {
        Initialize();
        GoBack();
        Modes = GetComponent<Button>();
        Modes.onClick.AddListener(ModesWindow);
        Back.onClick.AddListener(GoBack);
    }

    private void GoBack()
    {
        modeWindow.SetActive(false);
    }

    private void ModesWindow()
    {
        modeWindow.SetActive(true);
    }

    private void Update()
    {
        MultiplayerToggle();
        MapToggle();
    }

    void Initialize()
    {
        if (GameManager.gameMode == GameModes.MultiPlayer)
        {
            MultiplayerOption.isOn = true;
        }
        if (GameManager.mapMode == MapModes.Bounded)
        {
            BoundedOption.isOn = true;
        }
    }
    void MultiplayerToggle()
    {
        if(MultiplayerOption.isOn == true)
        {
            GameManager.gameMode = GameModes.MultiPlayer;
        }
        if (MultiplayerOption.isOn == false)
        {
            GameManager.gameMode = GameModes.SinglePlayer;
        }
    }

    void MapToggle()
    {
        if (BoundedOption.isOn == true)
        {
            GameManager.mapMode = MapModes.Bounded;
        }
        if (BoundedOption.isOn == false)
        {
            GameManager.mapMode = MapModes.Boundless;
        }
    }
}
