using Microsoft.MixedReality.Toolkit.UX;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshPro;

/*
 * TODO
 * 1. RAY + GAZE
*/

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // GAME LIFE CYCLE STATE
    public GameState gameState;

    // GAME MODE SETTINGS
    public BallInteractMode ballInteractMode = BallInteractMode.HAND_RAY;
    // 倒數計時開始秒數
    public float countdownStartTime = 30f;

    // IN_GAME STATE
    public float countdownTime = 0f;
    public int hitBalls = 0;

    // score history 
    public List<ScoreHistory> scoreHistory = new List<ScoreHistory>();

    // UI
    public GameObject startUI;
    public GameObject gameUI;
    public GameObject historyUI;
    public Text CountDownText;
    public Text HitBallsText;
    public Text BallModeText;
    public PressableButton ToggleRayBtn;
    public PressableButton ToggleGazeBtn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        gameState = GameState.START_MENU;
        ballInteractMode = BallInteractMode.HAND_RAY;
        OnToggleBallMode(BallInteractMode.HAND_RAY);
        InitScoreHistory();
        startUI.SetActive(true);
        gameUI.SetActive(true);
        historyUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 若遊戲狀態為遊戲中，則開始倒數計時
        if (gameState == GameState.IN_GAME)
        {
            countdownTime -= Time.deltaTime;
            UpdateUI();
        }

        if (gameState== GameState.HISTORY_MENU)
        {
            return;
        }
        
        // 若倒數計時小於0，則遊戲結束
        if (countdownTime < 0)
        {
            OnGameEnd();
        }
    }

    public void OnGameStart()
    {

        // TODO: 先倒數3秒給玩家準備

        // 初始化遊戲狀態
        gameState = GameState.IN_GAME;
        countdownTime = countdownStartTime;
        hitBalls = 0;
        // 初始化球管理器
        BallManager.instance.InitNewGame();
        startUI.SetActive(false);
        gameUI.SetActive(true);
        historyUI.SetActive(false);
    }

    public void OnGamePause()
    {
        if(gameState==GameState.START_MENU|| gameState == GameState.HISTORY_MENU)
        {
            return;
        }
        gameState = GameState.IN_GAME_PAUSE;
    }

    public void OnGameResume()
    {
        if (gameState == GameState.START_MENU || gameState == GameState.HISTORY_MENU)
        {
            return;
        }
        gameState = GameState.IN_GAME;
    }

    public void OnGameEnd()
    {
        // 儲存此次遊玩成績
        gameState = GameState.HISTORY_MENU;
        SaveScore();
        startUI.SetActive(true);
        gameUI.SetActive(true);
        historyUI.SetActive(true);
    }

    public void OnViewHistory()
    {
        // 顯示歷史成績
        // TODO
        startUI.SetActive(false);
        gameUI.SetActive(true);
        historyUI.SetActive(true);
    }

    public void OnGotoHome()
    {
        // 回到主畫面
        gameState = GameState.START_MENU;
        OnUpdateCountDownStartTime(30f);
        hitBalls = 0;
        UpdateUI();
        startUI.SetActive(true);
        gameUI.SetActive(true);
        historyUI.SetActive(false);
    }

    // 儲存此次遊玩成績
    private void InitScoreHistory()
    {
        // TODO: 從DISK初始化history
    }
    private void SaveScore()
    {
        // TODO: 存到DISK
        scoreHistory.Add(new ScoreHistory(ballInteractMode, hitBalls, countdownStartTime));
    }
    
    private void UpdateUI()
    {
        float displayTime = (float)Math.Round(countdownTime, 1);
        CountDownText.text = $"{displayTime}";
        HitBallsText.text = $"{hitBalls}";
    }

    public void OnToggleBallMode(BallInteractMode newMode)
    {
        switch (newMode)
        {
            case BallInteractMode.HAND_RAY:

                // 若原本模式為GAZE，則切為BOTH
                if (ballInteractMode == BallInteractMode.GAZE_AND_PINCH)
                {
                    ballInteractMode = BallInteractMode.BOTH;
                }
                else
                {
                    ballInteractMode = BallInteractMode.HAND_RAY;
                }
                break;
            case BallInteractMode.GAZE_AND_PINCH:
                // 若原本模式為GAZE，則切為BOTH
                if (ballInteractMode == BallInteractMode.HAND_RAY)
                {
                    ballInteractMode = BallInteractMode.BOTH;
                }
                else
                {
                    ballInteractMode = BallInteractMode.GAZE_AND_PINCH;
                }
                break;
            default:
                break;
        }
            OnUpdateBallModeText();
    }

    public void OnDetoggleBallMode(BallInteractMode newMode)
    {
        switch (newMode)
        {
            case BallInteractMode.HAND_RAY:
                // 若原本模式為HAND_RAY，則不做事
                if (ballInteractMode == BallInteractMode.HAND_RAY)
                {
                    ToggleRayBtn.ForceSetToggled(true);
                    return;
                }
                // 若原本模式為BOTH，則切為GAZE
                if (ballInteractMode == BallInteractMode.BOTH)
                {
                    ballInteractMode = BallInteractMode.GAZE_AND_PINCH;
                }
                break;
            case BallInteractMode.GAZE_AND_PINCH:
                // 若原本模式為GAZE_AND_PINCH，則不做事
                if (ballInteractMode == BallInteractMode.GAZE_AND_PINCH)
                {
                    ToggleGazeBtn.ForceSetToggled(true);
                    return;
                }
                // 若原本模式為BOTH，則切為GAZE
                if (ballInteractMode == BallInteractMode.BOTH)
                {
                    ballInteractMode = BallInteractMode.HAND_RAY;
                }
                break;
            default:
                break;
        }
        OnUpdateBallModeText();
    }

    public void OnUpdateBallModeText()
    {
        switch (ballInteractMode)
        {
            case BallInteractMode.HAND_RAY:
                BallModeText.text = "RAY";
                break;
            case BallInteractMode.GAZE_AND_PINCH:
                BallModeText.text = "GAZE";
                break;
            case BallInteractMode.BOTH:
                BallModeText.text = "BOTH";
                break;
            default:
                break;
        }
    }
    public void OnUpdateBallMode(BallInteractMode newMode)
    {
        switch (newMode)
        {
            case BallInteractMode.HAND_RAY:
                BallModeText.text = "RAY";
                break;
            case BallInteractMode.GAZE_AND_PINCH:
                BallModeText.text = "GAZE";
                break;
            case BallInteractMode.BOTH:
                BallModeText.text = "BOTH";
                break;
            default:
                break;
        }
    }
    public void OnUpdateCountDownStartTime(float time)
    {
        countdownStartTime = time;
        countdownTime = time;
        CountDownText.text = $"{time}s";
    }
}

public enum GameState
{
    START_MENU,
    IN_GAME,
    IN_GAME_PAUSE,
    HISTORY_MENU
}

public enum BallInteractMode
{
    HAND_RAY,
    GAZE_AND_PINCH,
    BOTH
}

public class ScoreHistory
{
    public BallInteractMode ballInteractMode { get; set; }
    public int hitBalls { get; set; }

    public float countdownStartTime { get; set; }

    public ScoreHistory(BallInteractMode ballInteractMode, int hitBalls, float countdownStartTime)
    {
        this.ballInteractMode = ballInteractMode;
        this.hitBalls = hitBalls;
        this.countdownStartTime = countdownStartTime;
    }
}

public class Util
{
    public static string GetBallInteractModeString(BallInteractMode ballInteractMode)
    {
        switch (ballInteractMode)
        {
            case BallInteractMode.HAND_RAY:
                return "RAY";
            case BallInteractMode.GAZE_AND_PINCH:
                return "GAZE";
            case BallInteractMode.BOTH:
                return "BOTH";
            default:
                return "RAY";
        }
    }
}