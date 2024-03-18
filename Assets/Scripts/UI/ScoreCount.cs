using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public float playerScoreCount;
    public Text scoreText;

    void Update(){
        ScoreIncrease();
        UpdateScoreText();
        }
    private void ScoreIncrease(){
        playerScoreCount += Time.deltaTime;
    }
    private void UpdateScoreText(){
        int scoreInt = Mathf.FloorToInt(playerScoreCount);
        scoreText.text = "SCORE " + scoreInt;
    }
    private bool SaveHighScore(int newScore){
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        bool gotNewHighscore = newScore > highScore;
        if(gotNewHighscore){
            PlayerPrefs.SetInt("HighScore", newScore);
            PlayerPrefs.Save();
            Debug.Log(newScore);
        }
        return gotNewHighscore;
    }
}
