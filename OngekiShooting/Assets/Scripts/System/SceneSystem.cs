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
    [SerializeField, Header("ロード時に再生するBGM")]
    int firstBGM = 0;
    [SerializeField, Header("ボスが出てくるまでの時間")]
    float bossTime = 100f;

    SoundManager soundManager;
    bool previousIsDead;
    bool previousIsBossDead;

    float timer;
    bool previousBoss;

    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlayBgm(firstBGM);
        previousIsBossDead = true;
        previousIsDead = true;
        Fade.FadeIn();
        if (sceneType == SceneType.GamePlay)
            timer = 0;
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
        previousBoss = IsBoss();

        if (sceneType == SceneType.GamePlay)
            timer += Time.deltaTime;
        if (!previousBoss && IsBoss())
            StartCoroutine(BossCoroutine());
    }

    bool IsBoss()
    {
        return timer > bossTime;
    }

    void LoadScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }

    public void FadeLoad(string nextScene)
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager?.SetFade();
        Fade.FadeOut(nextScene);
    }

    public void LoadGamePlay()
    {
        InitState();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.PlaySe(0);
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
        soundManager.PlayBgm(2);
        SceneState.isGameOver = true;
    }

    IEnumerator ClearCoroutine()
    {
        yield return new WaitForSeconds(clearDelay);
        SceneState.isBossDead = true;
        SceneState.isClear = false;
        LoadResult();
    }

    IEnumerator BossCoroutine()
    {
        soundManager.SetFade();
        yield return new WaitForSeconds(soundManager.fadeOutTime);
        soundManager.PlayBgm(3);
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

