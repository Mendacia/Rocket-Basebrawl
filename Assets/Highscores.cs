﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour
{
    const string privateCode = "JjKJ1tcNEkKY-tkdYaDp6wNk5zuudnHEOkswpYZMG3cQ";
    const string publicCode = "606d036a8f421366b065719c";
    const string webURL = "http://dreamlo.com/lb/";

    [SerializeField] private string playerName;
    public Highscore[] highscoresList;
    [SerializeField] private SavingScript save;
    [SerializeField] private Text name;

    [SerializeField] private Animator Leaderboard;
    [SerializeField] private GameObject EnterName, LeaderboardEmpty;

    [SerializeField] private List<GameObject> leaderboardPlacesParents = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            RetrieveHighScores();
        }
    }




    public void AddNewHighscore()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            StartCoroutine(UploadNewHighScore(playerName, save.loadInt("score", 0)));
            StartCoroutine(animationSyncer());
        }
    }
    IEnumerator UploadNewHighScore(string user, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(user) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            RetrieveHighScores();
        }
        else
        {
            print("Error Uploading" + www.error);
        }
    }

    public void RetrieveHighScores()
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

            if(i < 6)
            {
                leaderboardPlacesParents[i].transform.GetChild(0).GetComponent<Text>().text = highscoresList[i].score.ToString();
                leaderboardPlacesParents[i].transform.GetChild(1).GetComponent<Text>().text = highscoresList[i].username;
            }
        }
    }


    public void ChangePlayerName()
    {
        StartCoroutine(UpdateName());
    }
    IEnumerator UpdateName()
    {
        yield return new WaitForSeconds(0.1f);
        playerName = name.text;
    }

    IEnumerator animationSyncer()
    {
        Leaderboard.SetTrigger("Swap");
        yield return new WaitForSeconds(0.3333f);
        EnterName.SetActive(false);
        LeaderboardEmpty.SetActive(true);
        Leaderboard.SetTrigger("Go");
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
