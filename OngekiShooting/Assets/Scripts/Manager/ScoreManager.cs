using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore;
    [SerializeField, Header("スコア表示テキスト")]
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
    }

    private void Update()
    {
        scoreText.text = currentScore.ToString("D7");
    }

    public static void AddScore(int score)
    {
        currentScore += score;
    }
}
