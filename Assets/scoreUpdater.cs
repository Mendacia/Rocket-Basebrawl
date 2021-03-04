using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreUpdater : MonoBehaviour
{
    private scoreHolder myScoreHolder;
    [System.NonSerialized] public int ballIndex;
    [Header("This needs to be set to the fielding team to manipulate the UI")]
    [SerializeField] private fielderPeltingScript peltingScript = null;
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
    private int comboCount = 0;
    private float totalUnbankedBalls = 0;
    private float unstableScore = 0;

    //This gets sent off to the scoreHolder for use in the end screen
    private int totalSilver;
    private int totalGold;
    private int totalMiss;
    private int maxCombo;

    void Start()
    {
        myScoreHolder = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
    }

    public void HitAddToScore(bool pitchingPhase)
    {

        unstableScore += (defaultScore + comboCount * hitComboIncrament);
        comboCount++;
        if (comboCount > maxCombo)
        {
            maxCombo = comboCount;
        }
        totalUnbankedBalls++;
        totalSilver++;

        if (!pitchingPhase)
        {
            //Changing Texture
            var tempBall = peltingScript.upcomingBallList[ballIndex];
            tempBall.myTexture = BallIconHolder.GetIcon(BallResult.SILVER, peltingScript.upcomingBallList[ballIndex].myTauntLevel);
            tempBall.uIIcon.GetComponentInChildren<Image>().sprite = tempBall.myTexture;
            peltingScript.upcomingBallList[ballIndex] = tempBall;
        }
    }

    public void SweetAddToScore(bool pitchingPhase)
    {
        Debug.Log("Fired");
        unstableScore += (defaultScore * 1.25f) + (comboCount * sweetComboIncrament);
        comboCount++;
        if (comboCount > maxCombo)
        {
            maxCombo = comboCount;
        }
        totalUnbankedBalls++;
        totalGold++;

        if (!pitchingPhase)
        {
            //Changing Texture
            var tempBall = peltingScript.upcomingBallList[ballIndex];
            tempBall.myTexture = BallIconHolder.GetIcon(BallResult.GOLD, peltingScript.upcomingBallList[ballIndex].myTauntLevel);
            tempBall.uIIcon.GetComponentInChildren<Image>().sprite = tempBall.myTexture;
            peltingScript.upcomingBallList[ballIndex] = tempBall;
        }
    }

    public void BonkAddToScore()
    {
        unstableScore += (defaultScore / 2) + (comboCount * bonkComboIncrament);
    }

    public void SubtractFromScore()
    {
        unstableScore = totalUnbankedBalls * defaultScore;
        myScoreHolder.score -= 1000;
        comboCount = 0;
        totalMiss++;

        
        var tempBall = peltingScript.upcomingBallList[ballIndex];
        tempBall.myTexture = BallIconHolder.GetIcon(BallResult.MISS, peltingScript.upcomingBallList[ballIndex].myTauntLevel);
        tempBall.uIIcon.GetComponentInChildren<Image>().sprite = tempBall.myTexture;
        peltingScript.upcomingBallList[ballIndex] = tempBall;
    }

    public void BankScore()
    {
        myScoreHolder.UpdateScore(unstableScore);
        unstableScore = 0;
        comboCount = 0;
        totalUnbankedBalls = 0;
    }

    public void SendNumbersOverToTheScoreHolder()
    {
        myScoreHolder.StoreVariablesFromGameplay(totalSilver, totalGold, totalMiss, maxCombo);
    }

    private void Update()
    {
        unstableScoreText.text = unstableScore.ToString();
        if (comboCount > 1)
        {
            comboText.text = "COMBO " + comboCount.ToString() + "!";
        }
        else
        {
            comboText.text = "";
        }
    }
}
