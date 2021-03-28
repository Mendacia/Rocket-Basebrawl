using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderMultiBeamParent : MonoBehaviour
{
    public bool missed = false;
    public bool gold = false;
    private scoreUpdater myScoreUpdater;
    private BallList myBallList;
    public int index;

    private void Awake()
    {
        myScoreUpdater = GameObject.Find("ScoreUpdater").GetComponent<scoreUpdater>();
        myBallList = GameObject.Find("BallGod").GetComponent<BallList>();
    }

    private void Update()
    {
        if(transform.childCount == 0)
        {
            if (missed)
            {
                myScoreUpdater.SubtractFromScore();
                myBallList.SetToMiss(index);
            }
            else if (gold)
            {
                myScoreUpdater.SweetAddToScore();
                myBallList.SetToGold(index);
            }
            else
            {
                myScoreUpdater.HitAddToScore();
                myBallList.SetToSilver(index);
            }

            Destroy(gameObject);
        }
    }
}
