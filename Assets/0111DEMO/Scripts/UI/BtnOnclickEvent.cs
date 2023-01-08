using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOnclickEvent : MonoBehaviour
{
    public void OnToggleRay()
    {
        GameManager.instance.OnToggleBallMode(BallInteractMode.HAND_RAY);
    }
    public void OnDeToggleRay()
    {
        GameManager.instance.OnDetoggleBallMode(BallInteractMode.HAND_RAY);
    }
    public void OnToggleGaze()
    {
        GameManager.instance.OnToggleBallMode(BallInteractMode.GAZE_AND_PINCH);
    }
    public void OnDeToggleGaze()
    {
        GameManager.instance.OnDetoggleBallMode(BallInteractMode.GAZE_AND_PINCH);
    }
    public void OnSetHandRay()
    {
        GameManager.instance.OnUpdateBallMode(BallInteractMode.HAND_RAY);
    }
    public void OnSetGaze()
    {
        GameManager.instance.OnUpdateBallMode(BallInteractMode.GAZE_AND_PINCH);
    }

    public void OnSet10Sec()
    {
        GameManager.instance.OnUpdateCountDownStartTime(10f);
    }

    public void OnSet30Sec()
    {
        GameManager.instance.OnUpdateCountDownStartTime(30f);
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
