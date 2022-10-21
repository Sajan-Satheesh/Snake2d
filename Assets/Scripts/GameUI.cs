using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Button playAgain;
    public Button mainMenu;

    private void Awake()
    {
        playAgain.onClick.AddListener(PlayAgain);
        mainMenu.onClick.AddListener(GetMainMenu);
    }


    private void GetMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
