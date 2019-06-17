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
        if(Input.GetKeyDown(KeyCode.Alpha1))
        LoadScene("GamePlay");
    }

    void GamePlayLoad()
    {
        if (sceneType != SceneType.GamePlay) return;
        if(Input.GetKeyDown(KeyCode.Alpha1))
        LoadScene("Result");
        if(Input.GetKeyDown(KeyCode.Alpha2))
        LoadScene("GameOver");
    }

    void ResultLoad()
    {
        if (sceneType != SceneType.Result) return;
        if(Input.GetKeyDown(KeyCode.Alpha1))
        LoadScene("Ending");
    }
    void EndingLoad()
    {
        if (sceneType != SceneType.Ending) return;
        if(Input.GetKeyDown(KeyCode.Alpha1))
        LoadScene("Title");
    }

    void GameOverLoad()
    {
        if (sceneType != SceneType.GameOver) return;
        if(Input.GetKeyDown(KeyCode.Alpha1))
        LoadScene("Title");
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
