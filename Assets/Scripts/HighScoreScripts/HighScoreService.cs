
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using UnityEngine;

//public static class HighScoreService {

//    private static string url = "http://rollingball.azurewebsites.net/tables/HighScoreData";
//    #region private static string key = "..."
//    private static string key = "bueppEyJzkgHxQstVGyGjsvtirOKTH76";
//    #endregion

//    private static List<HighScoreData> scores;

//    public static List<HighScoreData> Scores
//    {
//        get
//        {
//            return scores;
//        }
//    }

//    public static IEnumerator GetScores(bool forceRetrieve = false)
//    {
//        if (forceRetrieve)
//        {
//            scores = null;
//        }

//        Dictionary<string, string> headers = new Dictionary<string, string>();
//        headers.Add("X-ZUMO-APPLICATION", key);
//        headers.Add("Content-Type", "application/json");
//        headers.Add("Accept", "application/json");
//        headers.Add("X-HTTP-Method-Override", "GET");
//        //Load High Scores from Azure Mobile Services
//        WWW www = new WWW(url + "?$orderby=score%20desc&$top=5", Encoding.UTF8.GetBytes("{}"), headers);
//        yield return www;

//        string data = "";
//        if (!string.IsNullOrEmpty(www.error))
//        {
//            Debug.Log("Error getting high scores: " + www.error);
//            data = "[]";
//        }
//        else
//        {
//            data = www.text;
//        }

//        scores = JsonConvert.DeserializeObject<List<HighScoreData>>(data);
//    }

//    public static IEnumerator SetScore(HighScoreData playerInfo)
//    {
//        string json = JsonConvert.SerializeObject(playerInfo);

//        Dictionary<string, string> headers = new Dictionary<string, string>();
//        headers.Add("X-ZUMO-APPLICATION", key);
//        headers.Add("Content-Type", "application/json");
//        headers.Add("Accept", "application/json");
//        headers.Add("X-HTTP-Method-Override", "GET");
//        WWW www = new WWW(url + "/" + playerInfo.id.ToString(), Encoding.UTF8.GetBytes("{}"), headers);
//        yield return www;

//        byte[] postData = Encoding.UTF8.GetBytes(json);
//        headers.Add("Content-Length", postData.Length.ToString());

//        if (!string.IsNullOrEmpty(www.error))
//        {
//            //Couldn't find high score
//            //Debug.Log("Couldn't find user: " + playerInfo.id.ToString() + " (" + playerInfo.playername + ")");

//            headers["X-HTTP-Method-Override"] = "POST";
//            //Do Insert of new player info
//            www = new WWW(url + "/", postData, headers);
//            yield return www;

//            //should check www.error for any errors on insert and maybe try again, or
//            //ask player if they want to wait so the game can try to submit their score again...
//        }
//        else
//        {
//            HighScoreData highScore = JsonConvert.DeserializeObject<HighScoreData>(www.text);

//            //Is the score good enough to replace old score?
//            if (playerInfo.score > highScore.score)
//            {
//                //Do Update of existing player info
//                headers["X-HTTP-Method-Override"] = "PATCH";
//                www = new WWW(url + "/" + playerInfo.id.ToString(), postData, headers);
//                yield return www;

//                //should check www.error for any errors on update and maybe try again, or
//                //ask player if they want to wait so the game can try to submit their score again...
//            }
//        }
//    }
//}
