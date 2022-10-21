using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayGame : MonoBehaviour
{
    Button play;
    private void Awake()
    {
        play = this.GetComponent<Button>();
        play.onClick.AddListener(PlaysGame);
    }

    private void PlaysGame()
    {
        SceneManager.LoadScene(1);
    }
}
