using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreUpdater : MonoBehaviour
{
    [SerializeField] private scoreHolder myScoreHolder;
    [SerializeField] private HUDManager myHUDManager;
    [Header("How much a single, no combo ball is worth")]
    [SerializeField] private int defaultScore = 1000;
    [Header("How much each action will increase per hit in combo")]
    [SerializeField] private int hitComboIncrament = 200;
    [SerializeField] private int sweetComboIncrament = 450;
    [SerializeField] private int bonkComboIncrament = 750;
    [Header("Set this to a text object on the UI")]
    [SerializeField] private Text unstableScoreText = null;
    [SerializeField] private Text comboText = null;

    [System.NonSerialized] public bool canScore = true;
    private int comboCount = 0;
    private int totalUnbankedBalls = 0;
    private int unstableScore = 0;

    //This gets sent off to the scoreHolder for use in the end screen
    private int totalSilver;
    private int totalGold;
    private int totalMiss;
    private int maxCombo;

    void Start()
    {
        myScoreHolder = GameObject.Find("DDOL_Scoreholder").GetComponent<scoreHolder>();
    }

    public void HitAddToScore(Vector3 target)
    {

        unstableScore += (defaultScore + comboCount * hitComboIncrament);
        comboCount++;
        if (comboCount > maxCombo)
        {
            maxCombo = comboCount;
        }
        if (comboCount > 1)
        {
            myHUDManager.displayTheComboElement(target);
        }
        totalUnbankedBalls++;
        totalSilver++;

        myHUDManager.SetTheTargetUnstableScore(unstableScore);
        myHUDManager.UpdateTheComboMultiplier(comboCount);
    }

    public void SweetAddToScore(Vector3 target)
    {
        Debug.Log("Fired");
        unstableScore += (int)(defaultScore * 1.25f) + (comboCount * sweetComboIncrament);
        comboCount++;
        if (comboCount > maxCombo)
        {
            maxCombo = comboCount;
        }
        if (comboCount > 1)
        {
            myHUDManager.displayTheComboElement(target);
        }
        totalUnbankedBalls++;
        totalGold++;

        myHUDManager.SetTheTargetUnstableScore(unstableScore);
        myHUDManager.UpdateTheComboMultiplier(comboCount);
    }

    public void BonkAddToScore()
    {
        unstableScore += (defaultScore / 2) + (comboCount * bonkComboIncrament);

        myHUDManager.SetTheTargetUnstableScore(unstableScore);
    }

    public void SubtractFromScore()
    {
        unstableScore = totalUnbankedBalls * defaultScore;
        myScoreHolder.score -= 1000;
        comboCount = 0;
        totalMiss++;

        myHUDManager.SetTheTargetUnstableScore(unstableScore);
        myHUDManager.UpdateTheComboMultiplier(comboCount);
    }

    public void BankScore()
    {
        myScoreHolder.UpdateScore(unstableScore);
        unstableScore = 0;
        comboCount = 0;
        totalUnbankedBalls = 0;
        myHUDManager.SetTheTargetUnstableScore(unstableScore);
        myHUDManager.UpdateTheComboMultiplier(comboCount);
    }

    public void SendNumbersOverToTheScoreHolder()
    {
        myScoreHolder.StoreVariablesFromGameplay(totalSilver, totalGold, totalMiss, maxCombo);
    }

    public int GetTheScoreUpdater() => unstableScore;
}
