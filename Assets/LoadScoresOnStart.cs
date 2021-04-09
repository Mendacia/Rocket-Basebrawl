using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScoresOnStart : MonoBehaviour
{
    [SerializeField] private Highscores leaderboardScript = null;
    void Start()
    {
        leaderboardScript.RetrieveHighScores();
    }
}
