using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreHolder : MonoBehaviour
{
    [System.NonSerialized] public float score = 0;

    [System.NonSerialized] public int mySilver;
    [System.NonSerialized] public int myGold;
    [System.NonSerialized] public int myMiss;
    [System.NonSerialized] public int myCombo;

    public static scoreHolder scoreStatic;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (scoreStatic == null)
        {
            scoreStatic = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateScore(float recievedScore)
    {
        score += recievedScore;
    }

    public void StoreVariablesFromGameplay(int silver, int gold, int miss, int combo)
    {
        mySilver = silver;
        myGold = gold;
        myMiss = miss;
        myCombo = combo;
    }
    //yeee
}
