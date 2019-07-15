using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    [SerializeField, Header("現在のシーンタイプ")]
    SceneType sceneType;
    //[SerializeField, Header("サウンドマネージャー")]
    //BGMManager bgmManager;
    [SerializeField, Header("イベントシステム")]
    EventSystem eventSystem;
    [SerializeField, Header("初めに選択するボタン")]
    Button selectButton;
    [SerializeField, Header("ゲームオーバー時のUI")]
    GameObject gameoverUI;
    [SerializeField, Header("ゲームオーバー表示までの時間")]
    float gameOverDelay = 1.0f;
    [SerializeField, Header("ゲームオーバー表示までの時間")]
    float clearDelay = 3.0f;

    bool previousIsDead;
    bool previousIsBossDead;

    private void Start()
    {
        //bgmManager.PlayBGM((int)sceneType);
        previousIsBossDead = true;
        previousIsDead = true;
        Fade.FadeIn();
    }

    private void InitState()
    {
        SceneState.isClear = false;
        SceneState.isDead = false;
        SceneState.isGameOver = false;
        SceneState.isBossDead = false;
    }

    private void Update()
    {
        CheckDead();
        CheckBossDead();
        previousIsDead = SceneState.isDead;
        previousIsBossDead = SceneState.isBossDead;
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

        FadeLoad("shota");
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

    void CheckBossDead()
    {
        if (previousIsBossDead || !SceneState.isBossDead) return;
        StartCoroutine(ClearCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneState.isGameOver = true;
    }

    IEnumerator ClearCoroutine()
    {
        yield return new WaitForSeconds(clearDelay);
        SceneState.isBossDead = true;
        SceneState.isClear = false;
        LoadResult();
    }

    enum SceneType
    {
        Title,
        GamePlay,
        Result,
    }
}

public static class SceneState
{
    public static bool isClear;
    public static bool isDead;
    public static bool isGameOver;
    public static bool isBossDead;
}

