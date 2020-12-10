using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fielderScoring : MonoBehaviour
{
    [SerializeField] private Text npcScoreUIText;

    [Header("Odds ratios for fielder normal success, critical success, and failure")]
    [SerializeField] private int fielderSuccessOdds;
    [SerializeField] private int fielderCritOdds;
    [SerializeField] private int fielderFailureOdds;

    [Header("Value ranges for fielder failure scores (Both min and max are inclusive)")]
    [SerializeField] private int failureMax;
    [SerializeField] private int failureMin;

    [Header("Value ranges for fielder success scores (Both min and max are inclusive)")]
    [SerializeField] private int successMax;
    [SerializeField] private int successMin;

    [Header("Value ranges for fielder critical scores (Both min and max are inclusive)")]
    [SerializeField] private int critMax;
    [SerializeField] private int critMin;

    private int fielderFinalScore;


    public void fielderScoreGenerator()
    {
        var typeOfHit = (" ");
        var myHitNumber = Random.Range(0, (fielderSuccessOdds + fielderCritOdds + fielderFailureOdds));

        if(myHitNumber < fielderFailureOdds)
        {
            typeOfHit = ("Failure");
            fielderFinalScore += Random.Range(failureMin, failureMax + 1);
        }
        else if(myHitNumber >= fielderFailureOdds && myHitNumber < fielderSuccessOdds + fielderFailureOdds)
        {
            typeOfHit = ("Success");
            fielderFinalScore += Random.Range(successMin, successMax + 1);
        }
        else if(myHitNumber >= fielderSuccessOdds + fielderFailureOdds)
        {
            typeOfHit = ("Crit");
            fielderFinalScore += Random.Range(critMin, critMax + 1);
        }

        npcScoreUIText.text = fielderFinalScore.ToString();
        Debug.Log(typeOfHit + (" ") + fielderFinalScore);
    }
}
