using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public static int scoreCount;

    //public int score = 0;

    //public int maxScore;

    // Start is called before the first frame update
    void Start()
    {
        //score = 0;
    }

    /*public void AddScore(int newScore)
    {
        score += newScore;
    }*/

    /*public void UpdateScore()
    {
        ScoreText.text = "Score 0" + score;
    }*/

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + Mathf.Round(scoreCount);

        //UpdateScore();
    }
}
