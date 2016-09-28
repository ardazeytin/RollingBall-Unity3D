//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;
//using System;

//public class HighScores : MonoBehaviour {

//    IEnumerator Start()
//    {
//        yield return StartCoroutine(HighScoreService.GetScores(forceRetrieve: true));

//        PopulateScores();
//    }

//    private void PopulateScores()
//    {
//        List<HighScoreData> scores = HighScoreService.Scores;

//        Text highscoreText = gameObject.GetComponent<Text>();
//        highscoreText.text = "";
//        int numberOfScores = Math.Min(scores.Count, 5);
//        for (int i = 0; i < numberOfScores; i++)
//        {
//            HighScoreData score = scores[i];
//            highscoreText.text += (i + 1).ToString() + ". " + score.score.ToString().PadLeft(4, '0') + " - " + score.playername + "\r\n";
//        }
//    }
//}
