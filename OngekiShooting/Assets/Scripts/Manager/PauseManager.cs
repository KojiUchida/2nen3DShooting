using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ポーズを開いているか判定用クラス
/// </summary>
public static class Pause
{
    public static bool isPause;
}

public class PauseManager : MonoBehaviour
{
    [SerializeField, Header("ポーズ画面のUI")]
    private GameObject pauseUI;

    [SerializeField, Header("ボタン選択加速までの秒数")]
    private float repeatDelay = 1f;

    [SerializeField, Header("1秒間に何回入力を受け入れるか")]
    private float selectInterval = 1f;
    [SerializeField, Header("タイトル画面")]
    private string title;

    [SerializeField, Header("未選択時の文字色")]
    private Color Unselected_textColor;
    [SerializeField, Header("選択時の文字色")]
    private Color Selected_textColor;

    [SerializeField, Header("決定時SE")]
    private AudioClip desideSE;
    [SerializeField, Header("キャンセルSE")]
    private AudioClip cancelSE;
    [SerializeField, Header("項目選択SE")]
    private AudioClip selectSE;

    int currentButton;
    int previousButton;

    bool isWait;
    bool isSelecct;

    float currentVertical;
    float previousVertical;

    SceneSystem sceneSystem;
    Button[] buttons;
    Text[] texts;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        PauseAndResume();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Init()
    {
        sceneSystem = FindObjectOfType<SceneSystem>();
        buttons = pauseUI.GetComponentsInChildren<Button>();
        texts = pauseUI.GetComponentsInChildren<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ポーズ呼び出しとゲームに戻る
    /// </summary>
    public void PauseAndResume()
    {
        previousVertical = currentVertical;
        currentVertical = Input.GetAxis("Vertical");
        Pausing();
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        //ポーズ処理
        if (!pauseUI.activeSelf) PauseStart();
        else Resume();
    }

    /// <summary>
    /// ポーズ中
    /// </summary>
    void Pausing()
    {
        if (!Pause.isPause) return;
        buttons[currentButton].Select();

        PressButton();

        //スティック入力
        if ((currentVertical > 0 || Input.GetKeyDown(KeyCode.UpArrow)) && !isWait && !isSelecct)
        {
            currentButton--;
            StartCoroutine(SelectInterval());
        }
        if ((currentVertical < 0 || Input.GetKeyDown(KeyCode.DownArrow)) && !isWait && !isSelecct)
        {
            currentButton++;
            StartCoroutine(SelectInterval());
        }
        if (currentVertical == 0)
        {
            StopCoroutine(Wait());
            StopCoroutine(SelectInterval());
            isWait = false;
            isSelecct = false;
        }

        if (previousVertical == 0 && currentVertical != 0)
            StartCoroutine(Wait());

        if (currentButton > buttons.Length - 1)
            currentButton = 0;
        if (currentButton < 0)
            currentButton = buttons.Length - 1;

        texts[previousButton].color = Unselected_textColor;
        texts[currentButton].color = Selected_textColor;

        previousButton = currentButton;
    }

    /// <summary>
    /// ポーズ
    /// </summary>
    void PauseStart()
    {
        Pause.isPause = !Pause.isPause;
        currentButton = 0;
        pauseUI.SetActive(!pauseUI.activeSelf);
        Time.timeScale = 0;
        PlaySelectSE();
    }

    /// <summary>
    /// 再開
    /// </summary>
    public void Resume()
    {
        Pause.isPause = !Pause.isPause;
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        PlayCancelSE();
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }

    /// <summary>
    /// 移動までのウェイト
    /// </summary>
    /// <returns></returns>
    IEnumerator Wait()
    {
        isWait = true;
        yield return new WaitForSeconds(repeatDelay);
        isWait = false;
    }

    /// <summary>
    /// 移動するときの間隔
    /// </summary>
    /// <returns></returns>
    IEnumerator SelectInterval()
    {
        PlaySelectSE();
        isSelecct = true;
        yield return new WaitForSeconds(selectInterval);
        isSelecct = false;
    }

    /// <summary>
    /// ボタンを押したときの処理
    /// </summary>
    void PressButton()
    {
        //ボタン入力
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) 
        {
            buttons[currentButton].onClick.Invoke();
            if (currentButton == 1 || currentButton == 2)
            {
                Pause.isPause = !Pause.isPause;
                Time.timeScale = 1;
            }

            PlayDesideSE();
        }
    }

    /// <summary>
    /// タイトルへ
    /// </summary>
    public void GoTitle()
    {
        if (title == null || sceneSystem == null) return;
        sceneSystem.LoadScene(title);
    }

    /// <summary>
    /// 決定SEを流す
    /// </summary>
    private void PlayDesideSE()
    {
        audioSource.PlayOneShot(desideSE);
    }

    /// <summary>
    /// キャンセルSEを流す
    /// </summary>
    private void PlayCancelSE()
    {
        audioSource.PlayOneShot(cancelSE);
    }

    /// <summary>
    /// 選択SEを流す
    /// </summary>
    private void PlaySelectSE()
    {
        audioSource.PlayOneShot(selectSE);
    }
}
