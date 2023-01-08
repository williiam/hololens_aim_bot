using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using Text = TMPro.TextMeshPro;

public class HistoryView : MonoBehaviour
{
    public Text HistoryScoreText;
    // Start is called before the first frame update
    void Start()
    {
        // 取得歷史紀錄
        List<ScoreHistory> history = GameManager.instance.scoreHistory;
        // 組成歷史紀錄字串 
        string historyStr = CreateStringFromList(history);
        HistoryScoreText.text = historyStr;
    }

    // Update is called once per frame
    void Update()
    {
        // 取得歷史紀錄
        List<ScoreHistory> history = GameManager.instance.scoreHistory;
        // 組成歷史紀錄字串 
        string historyStr = CreateStringFromList(history);
        HistoryScoreText.text = historyStr;
    }

    public string CreateStringFromList(List<ScoreHistory> list)
    {
        string result = "";
        list.Sort((x, y) => -x.hitBalls / x.countdownStartTime.CompareTo(y.hitBalls / y.countdownStartTime));


        // Iterate through the list and create the string
        int count = 1;
        foreach (ScoreHistory item in list)
        {
            string mode = Util.GetBallInteractModeString(item.ballInteractMode);

            // 取到小數點後一位
            float score = (float)System.Math.Round((double)item.hitBalls / item.countdownStartTime, 1);
            result += $"{count}. {mode} {item.hitBalls} HITS {item.countdownStartTime}s SCROE:{score}\n";
            count++;
        }

        return result;
    }
}
