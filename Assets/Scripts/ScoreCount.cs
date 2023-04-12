using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;
    
    public int score;

    [SerializeField] public float remainingTime;
    int startingTime;

    MenuController menuController;

    void Start()
    {
        menuController = FindAnyObjectByType<MenuController>();
        scoreText.text = "Cownt: 0";
        timerText.text = remainingTime.ToString();
    }

    private void Update()
    {
        Timer();

        if (score == 10 && remainingTime > 0)
        {
            winSequence();
        }

        if (score < 10 && remainingTime <= 0)
        {
            loseSequence();
        }
    }

    public void IncreaseScore(int scoreToIncrease)
    {
        score += scoreToIncrease;
        scoreText.text = $"Cownt: {score}";
    }

    public void Timer()
    {
        remainingTime -= 1 * Time.deltaTime;
        int time = Mathf.RoundToInt(remainingTime);
        timerText.text = time.ToString();
    }

    void winSequence()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void loseSequence()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
