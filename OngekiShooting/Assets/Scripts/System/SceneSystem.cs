using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    [SerializeField, Header("イベントシステム")]
    EventSystem eventSystem;
    [SerializeField, Header("初めに選択するボタン")]
    Button selectButton;
    [SerializeField, Header("ゲームオーバー時のUI")]
    GameObject gameoverUI;
    [SerializeField, Header("ゲームオーバー表示までの時間")]
    float DelayTime;

    bool previousIsDead;

    private void Start()
    {
        Fade.FadeIn();
    }

    private void InitState()
    {
        SceneState.isClear = false;
        SceneState.isDead = false;
        SceneState.isGameOver = false;
    }

    private void Update()
    {
        CheckDead();
        previousIsDead = SceneState.isDead;
        gameoverUI.SetActive(SceneState.isGameOver);
    }

    void LoadScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void FadeLoad(string nextScene)
    {
        Fade.FadeOut(nextScene);
    }

    public void LoadGamePlay()
    {
        InitState();

        FadeLoad("koji");
    }

    public void LoadResult()
    {
        FadeLoad("Result");
    }

    public void LoadTitle()
    {
        InitState();

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

    void CheckDead()
    {
        if (previousIsDead || !SceneState.isDead) return;
        StartCoroutine(GameOverCoroutine());
        eventSystem.SetSelectedGameObject(selectButton.gameObject);
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(DelayTime);
        SceneState.isGameOver = true;
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
    public static bool isGameOver;
}

