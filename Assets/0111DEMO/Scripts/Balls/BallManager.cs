using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    public GameObject rayBallPrefab;
    public GameObject gazeBallPrefab;
    public GameObject currentModeBallPrefab;
    public GameObject SpawnPoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentModeBallPrefab(BallInteractMode ballInteractMode)
    {
        switch (ballInteractMode)
        {
            case BallInteractMode.HAND_RAY:
                currentModeBallPrefab = rayBallPrefab;
                break;
            case BallInteractMode.GAZE_AND_PINCH:
                currentModeBallPrefab = gazeBallPrefab;
                break;
            default:
                currentModeBallPrefab = rayBallPrefab;
                break;
        }
    }

    public void InitNewGame()
    {
        // 清除所有球
        GameObject[] balls = GameObject.FindGameObjectsWithTag("BALL");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        
        // 隨機生成三顆球
        SpawnBall();
        SpawnBall();
        SpawnBall();
    }

    public void SpawnBall()
    {
        // 設定當前生成球種
        SetCurrentModeBallPrefab(GameManager.instance.ballInteractMode);

        // 取得spawn point位置
        Vector3 spawnPointPos = SpawnPoint.transform.position;

        // 以spawnPoint為中心一定範圍內隨機位置產生球
        Vector3 randomPos = new Vector3(Random.Range(spawnPointPos.x - 0.5f, spawnPointPos.x + 0.5f), Random.Range(spawnPointPos.y - 0.5f, spawnPointPos.y + 0.5f), Random.Range(spawnPointPos.z - 0.5f, spawnPointPos.z + 0.5f));
        Instantiate(currentModeBallPrefab, randomPos, Quaternion.identity);
    }

    // 當球被打中時，呼叫此方法
    public void OnBallHit()
    {
        if (GameManager.instance.gameState == GameState.IN_GAME)    
        {
            GameManager.instance.hitBalls++;
            SpawnBall();
        }
    }
}



