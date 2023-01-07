using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshPro;

/*
 * TODO
 * 1. GAZE�� disable �⪺rayCast
 * 2. GAZE�� ONHOVER �ܤj?
 * FIXME
 * 1. MENU�n��
*/

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // GAME LIFE CYCLE STATE
    public GameState gameState;

    // GAME MODE SETTINGS
    public BallInteractMode ballInteractMode = BallInteractMode.HAND_RAY;
    // �˼ƭp�ɶ}�l���
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
        startUI.SetActive(true);
        gameUI.SetActive(true);
        historyUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �Y�C�����A���C�����A�h�}�l�˼ƭp��
        if (gameState == GameState.IN_GAME)
        {
            countdownTime -= Time.deltaTime;
            UpdateUI();
        }

        if (gameState== GameState.HISTORY_MENU)
        {
            return;
        }
        
        // �Y�˼ƭp�ɤp��0�A�h�C������
        if (countdownTime < 0)
        {
            OnGameEnd();
        }
    }

    public void OnGameStart()
    {

        // TODO: ���˼�3�����a�ǳ�

        // ��l�ƹC�����A
        gameState = GameState.IN_GAME;
        countdownTime = countdownStartTime;
        hitBalls = 0;
        // ��l�Ʋy�޲z��
        BallManager.instance.InitNewGame();
        startUI.SetActive(false);
        gameUI.SetActive(true);
        historyUI.SetActive(false);
    }

    public void OnGamePause()
    {
        gameState = GameState.IN_GAME_PAUSE;
    }

    public void OnGameResume()
    {
        gameState = GameState.IN_GAME;
    }

    public void OnGameEnd()
    {
        // �x�s�����C�����Z
        gameState = GameState.HISTORY_MENU;
        SaveScore();
        startUI.SetActive(true);
        gameUI.SetActive(true);
        historyUI.SetActive(true);
    }

    public void OnViewHistory()
    {
        // ��ܾ��v���Z
        // TODO
        startUI.SetActive(false);
        gameUI.SetActive(true);
        historyUI.SetActive(true);
    }

    public void OnGotoHome()
    {
        // �^��D�e��
        gameState = GameState.START_MENU;
        startUI.SetActive(true);
        gameUI.SetActive(true);
        historyUI.SetActive(false);
    }

    private void SaveScore()
    {
        // �x�s�����C�����Z
        scoreHistory.Add(new ScoreHistory(ballInteractMode, hitBalls, countdownStartTime));
    }
    
    private void UpdateUI()
    {
        float displayTime = (float)Math.Round(countdownTime, 1);
        CountDownText.text = $"{displayTime}";
        HitBallsText.text = $"{hitBalls}";
    }

    public void OnUpdateBallMode()
    {
        switch (ballInteractMode)
        {
            case BallInteractMode.HAND_RAY:
                BallModeText.text = "RAY";
                break;
            case BallInteractMode.GAZE_AND_PINCH:
                BallModeText.text = "GAZE";
                break;
            default:
                break;
        }
    }
    public void OnUpdateCountDownTime(float time)
    {
        countdownStartTime = time;
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
    GAZE_AND_PINCH
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
            default:
                return "RAY";
        }
    }
}