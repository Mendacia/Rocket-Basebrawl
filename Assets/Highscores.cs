using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour
{
    const string privateCode = "JjKJ1tcNEkKY-tkdYaDp6wNk5zuudnHEOkswpYZMG3cQ";
    const string publicCode = "606d036a8f421366b065719c";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;

    private void Awake()
    {
        AddNewHighscore("Pepsi", 5);
        DownloadHighScoresRightFuckingNow();
    }

    public void AddNewHighscore(string user, int score)
    {
        StartCoroutine(UploadNewHighScore(user, score));
    }


    IEnumerator UploadNewHighScore(string user, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(user) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            print("Upload Successful");
        else
        {
            print("Error Uploading" + www.error);
        }
    }

    public void DownloadHighScoresRightFuckingNow()
    {
        StartCoroutine(DownloadHighScores());
    }

    IEnumerator DownloadHighScores()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            FormatHighScores(www.text);
        else
        {
            print("Error Downloading" + www.error);
        }
    }

    public void FormatHighScores(string textstream)
    {
        string[] entries = textstream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}
