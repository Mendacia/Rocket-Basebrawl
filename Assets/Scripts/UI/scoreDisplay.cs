using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreDisplay : MonoBehaviour
{
    [System.NonSerialized] public scoreHolder theAllSeeingScoreHolder;
    [SerializeField] private Text silverText = null;
    [SerializeField] private Text goldText = null;
    [SerializeField] private Text missText = null;
    [SerializeField] private Text comboText = null;

    void Start()
    {
        theAllSeeingScoreHolder = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
    }

    void Update()
    {
        gameObject.GetComponent<Text>().text = "Score: " + theAllSeeingScoreHolder.score;

        if (silverText != null)
        {
            silverText.text = "SILVER: " + theAllSeeingScoreHolder.mySilver;
            goldText.text = "GOLD: " + theAllSeeingScoreHolder.myGold;
            missText.text = "MISSES: " + theAllSeeingScoreHolder.myMiss;
            comboText.text = "MAX COMBO: " + theAllSeeingScoreHolder.myCombo;
        }
    }
}
