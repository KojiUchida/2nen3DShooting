using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    [SerializeField, Header("現在のシーンタイプ")]
    SceneType sceneType;

    private void Start()
    {
        Fade.FadeIn();
    }

    private void Update()
    {
        GamePlayLoad();
        ResultLoad();
        EndingLoad();
        GameOverLoad();
    }

    void LoadScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);

    }

    public void FadeLoad(string nextScene)
    {
        Fade.FadeOut(nextScene);
        //Fade.FadeIn();
        //SceneManager.LoadScene(nextScene);
    }

    public void TitleLoad()
    {
        if (sceneType != SceneType.Title) return;

        FadeLoad("koji");
    }

    public void GamePlayLoad()
    {
        if (sceneType != SceneType.GamePlay) return;

        if (SceneState.isClear || SceneState.isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                FadeLoad("Title");
                SceneState.isClear = false;
                SceneState.isDead = false;
            }
        }
    }

    public void ResultLoad()
    {
        if (sceneType != SceneType.Result) return;
        if (!Input.GetKeyDown(KeyCode.Alpha1)) return;

        FadeLoad("Ending");
    }
    public void EndingLoad()
    {
        if (sceneType != SceneType.Ending) return;
        if (!Input.GetKeyDown(KeyCode.Alpha1)) return;

        FadeLoad("Title");
    }

    public void GameOverLoad()
    {
        if (sceneType != SceneType.GameOver) return;
        if (!Input.GetKeyDown(KeyCode.Alpha1)) return;

        FadeLoad("Title");
    }

    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
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

public static class SceneState
{
    public static bool isClear;
    public static bool isDead;
}

