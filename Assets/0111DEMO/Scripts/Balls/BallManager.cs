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
        // �M���Ҧ��y
        GameObject[] balls = GameObject.FindGameObjectsWithTag("BALL");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
        
        // �H���ͦ��T���y
        SpawnBall();
        SpawnBall();
        SpawnBall();
    }

    public void SpawnBall()
    {
        // �]�w��e�ͦ��y��
        SetCurrentModeBallPrefab(GameManager.instance.ballInteractMode);

        // ���ospawn point��m
        Vector3 spawnPointPos = SpawnPoint.transform.position;

        // �HspawnPoint�����ߤ@�w�d���H����m���Ͳy
        Vector3 randomPos = new Vector3(Random.Range(spawnPointPos.x - 0.5f, spawnPointPos.x + 0.5f), Random.Range(spawnPointPos.y - 0.5f, spawnPointPos.y + 0.5f), Random.Range(spawnPointPos.z - 0.5f, spawnPointPos.z + 0.5f));
        Instantiate(currentModeBallPrefab, randomPos, Quaternion.identity);
    }

    // ��y�Q�����ɡA�I�s����k
    public void OnBallHit()
    {
        if (GameManager.instance.gameState == GameState.IN_GAME)    
        {
            GameManager.instance.hitBalls++;
            SpawnBall();
        }
    }
}



