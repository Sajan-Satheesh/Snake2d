using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Text Score;
    public Text CountDown;
    public int score;
    public int timer;

    private void Start()
    {
        UpdateScore();
        CountDown.enabled = false;
    }

    public void UpdateScore()
    {
        string update = "Score :" + score; ;
        Score.text = update;
    }

    public void CountDownTimer()
    {
        CountDown.text = "Timer : "+timer;
    }
}
