using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore;
    [SerializeField, Header("スコア表示テキスト")]
    Text scoreText;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    private void Update()
    {
        score++;
        score = Mathf.Clamp(score, 0, currentScore);
        scoreText.text = score.ToString("D7");
    }

    public static void AddScore(int score)
    {
        currentScore += score;
    }
}
