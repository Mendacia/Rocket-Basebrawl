using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreHolder : MonoBehaviour
{
    [System.NonSerialized] public float score = 0;
    [System.NonSerialized] public bool canScore = true;

    private static scoreHolder scoreStatic;

    private void Awake()
    {
       // DontDestroyOnLoad(this);

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
}
