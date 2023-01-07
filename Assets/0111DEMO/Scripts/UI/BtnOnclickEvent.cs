using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOnclickEvent : MonoBehaviour
{
    public void OnSetHandRay()
    {
        GameManager.instance.ballInteractMode = BallInteractMode.HAND_RAY;
        GameManager.instance.OnUpdateBallMode();
    }
    public void OnSetGaze()
    {
        GameManager.instance.ballInteractMode = BallInteractMode.GAZE_AND_PINCH;
        GameManager.instance.OnUpdateBallMode();
    }

    public void OnSet10Sec()
    {
        GameManager.instance.OnUpdateCountDownTime(10f);
    }

    public void OnSet30Sec()
    {
        GameManager.instance.OnUpdateCountDownTime(30f);
    }

    public void OnGameStart()
    {
        GameManager.instance.OnGameStart();
    }
    public void OnGamePause()
    {
        GameManager.instance.OnGamePause();
    }
    public void OnGameResume()
    {
        GameManager.instance.OnGameResume();
    }

    public void OnGoHome()
    {
        GameManager.instance.OnGotoHome();
    }
    public void OnViewHistory()
    {
        GameManager.instance.OnViewHistory();
    }
}
