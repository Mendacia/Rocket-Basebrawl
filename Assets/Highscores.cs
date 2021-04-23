using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour
{
    const string privateCode = "JjKJ1tcNEkKY-tkdYaDp6wNk5zuudnHEOkswpYZMG3cQ";
    const string publicCode = "606d036a8f421366b065719c";
    const string webURL = "http://dreamlo.com/lb/";

    [SerializeField] private string playerName = null;
    public Highscore[] highscoresList;
    [SerializeField] private Text NameField = null;

    [SerializeField] private Animator Leaderboard = null;
    [SerializeField] private GameObject EnterName, LeaderboardEmpty = null;

    [SerializeField] private List<GameObject> leaderboardPlacesParents = new List<GameObject>();
    [SerializeField] private GameObject returnButton = null;

    [SerializeField] private List<string> disallowedWords = new List<string>();

    private void Awake()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
    }

    public void AddNewHighscore()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            foreach(string word in disallowedWords)
            {
                if(playerName == word)
                {
                    return;
                }
            }
            StartCoroutine(UploadNewHighScore(playerName, (int)scoreHolder.scoreStatic.score)) ;
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
            Destroy(scoreHolder.scoreStatic.gameObject);
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
        playerName = NameField.text.ToUpper();
    }

    IEnumerator animationSyncer()
    {
        Leaderboard.SetTrigger("Swap");
        yield return new WaitForSeconds(0.3333f);
        EnterName.SetActive(false);
        LeaderboardEmpty.SetActive(true);
        Leaderboard.SetTrigger("Go");
        StartCoroutine(enableReturnButton());
    }

    IEnumerator enableReturnButton()
    {
        yield return new WaitForSeconds(3);
        returnButton.SetActive(true);
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
