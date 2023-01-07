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
        // ¨ú±o¾ú¥v¬ö¿ý
        List<ScoreHistory> history = GameManager.instance.scoreHistory;
        // ²Õ¦¨¾ú¥v¬ö¿ý¦r¦ê 
        string historyStr = CreateStringFromList(history);
        HistoryScoreText.text = historyStr;
    }

    // Update is called once per frame
    void Update()
    {
        // ¨ú±o¾ú¥v¬ö¿ý
        List<ScoreHistory> history = GameManager.instance.scoreHistory;
        // ²Õ¦¨¾ú¥v¬ö¿ý¦r¦ê 
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
            result += $"{count}. {mode} {item.countdownStartTime}s {item.hitBalls} HITS \n";
            count++;
        }

        return result;
    }
}
