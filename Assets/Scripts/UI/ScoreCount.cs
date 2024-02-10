using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public float playerScoreCount;
    public Text scoreText;

    void Update()
    {
        ScoreIncrease();
        UpdateScoreText();
    }

    private void ScoreIncrease()
    {
        playerScoreCount += Time.deltaTime;
    }
    
    private void UpdateScoreText()
    {
        int scoreInt = Mathf.FloorToInt(playerScoreCount);
        scoreText.text = "SCORE " + scoreInt;
    }
}
