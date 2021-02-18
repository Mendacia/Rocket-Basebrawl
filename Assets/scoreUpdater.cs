using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreUpdater : MonoBehaviour
{
    private scoreHolder myScoreHolder;
    [Header("How much a single, no combo ball is worth")]
    [SerializeField] private float defaultScore = 1000;
    [Header("How much each action will increase per hit in combo")]
    [SerializeField] private float hitComboIncrament = 200;
    [SerializeField] private float sweetComboIncrament = 450;
    [SerializeField] private float bonkComboIncrament = 750;
    [Header("Set this to a text object on the UI")]
    [SerializeField] private Text unstableScoreText = null;
    [SerializeField] private Text comboText = null;

    [System.NonSerialized] public bool canScore =true;
    private float comboCount = 0;
    private float totalUnbankedBalls = 0;
    private float unstableScore = 0;
    void Start()
    {
        myScoreHolder = gameObject.GetComponent<scoreHolder>();
    }

    public void HitAddToScore ()
    {
        unstableScore += (defaultScore + comboCount * hitComboIncrament);
        comboCount++;
        totalUnbankedBalls++;
    }

    public void SweetAddToScore()
    {
        unstableScore += (defaultScore * 1.25f) + (comboCount * sweetComboIncrament);
        comboCount++;
    }

    public void BonkAddToScore()
    {
        unstableScore += (defaultScore / 2) + (comboCount * bonkComboIncrament);
    }

    public void SubtractFromScore ()
    {
        unstableScore = totalUnbankedBalls * defaultScore;
        myScoreHolder.score -= 1000;
        comboCount = 0;
    }

    public void BankScore()
    {
        myScoreHolder.UpdateScore(unstableScore);
        unstableScore = 0;
        comboCount = 0;
        totalUnbankedBalls = 0;
    }

    private void Update()
    {
        unstableScoreText.text = unstableScore.ToString();
        if (comboCount > 1)
        {
            comboText.text = "COMBO " + comboCount.ToString() + "!";
        }
        else
            comboText.text = "";
    }
}
