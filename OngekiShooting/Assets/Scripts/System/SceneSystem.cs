using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    [SerializeField, Header("現在のシーンタイプ")]
    SceneType sceneType;

    private void Update()
    {
        TitleLoad();
        GamePlayLoad();
        ResultLoad();
        EndingLoad();
        GameOverLoad();
    }

    public void LoadScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    void TitleLoad()
    {
        if (sceneType != SceneType.Title) return;
        LoadScene("Game");
    }

    void GamePlayLoad()
    {
        if (sceneType != SceneType.GamePlay) return;
        LoadScene("Result");
    }

    void ResultLoad()
    {
        if (sceneType != SceneType.Result) return;
        LoadScene("Ending");
    }
    void EndingLoad()
    {
        if (sceneType != SceneType.Ending) return;
        LoadScene("Title");
    }

    void GameOverLoad()
    {
        if (sceneType != SceneType.Ending) return;
        LoadScene("GameOver");
    }

    enum SceneType
    {
        Title,
        GamePlay,
        Result,
        Ending,
        GameOver,
    }
}
