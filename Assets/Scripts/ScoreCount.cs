using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    public int score;
    [SerializeField] TMP_Text scoreText;

    void Start()
    {
        //scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Cows grabbed: 0";
    }

    public void IncreaseScore(int scoreToIncrease)
    {
        score += scoreToIncrease;
        scoreText.text = $"Cows grabbed: {score}";
    }
}
