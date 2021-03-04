using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallList : MonoBehaviour
{
    public enum ballState
    {
        INACTIVE,
        ACTIVE,
        MISSED,
        SILVER,
        GOLD
    }

    public enum ballType
    {
        STANDARD,
        ARC,
        SCATTER,
        MULTI
    }
    
    [System.Serializable]public struct masterBallStruct
    {
        public int myIndex;                      //This ball's place in both this script's List and fielderPeltingScript's List
        public ballType myType;               //Type of throw the fielders will use when firing this ball
        public ballState currentState;         //Mostly used for UI stuff but also tells the ball when it's active
        public int myTauntLevel;               //The level of taunt assigned to this ball. Manages other variables
        public GameObject uIObject;          //The object on the UI that handles the sprite
        public Transform myFielder;          //The fielder that will throw this ball
        public float myThrowSpeed;          //Speed at which beam decreases in size
        public float myReadySpeed;          //Time between last ball and this ball
        public int extraBallCount;              //Extra balls for multi and scatter
    }
    public List<masterBallStruct> masterBallList;


    //Between this and the next comment is entirely setup of variables for the ball list
    public void AddThisBallToTheList(int Index, int taunt)
    {
        masterBallStruct thisBall = new masterBallStruct();
        thisBall.myIndex = Index;
        thisBall.myTauntLevel = taunt;
        masterBallList.Add(thisBall);
    }

    public void AssignRemainingVariables()
    {
        foreach(masterBallStruct ball in masterBallList)
        {
            SetType(ball);

            switch (ball.myType)
            {
                case ballType.STANDARD:
                    StandardBallSetup(ball);
                    return;

                case ballType.ARC:
                    ArcBallSetup(ball);
                    return;

                case ballType.MULTI:
                    MultiBallSetup(ball);
                    return;

                case ballType.SCATTER:
                    ScatterBallSetup(ball);
                    return;
            }
        }
    }

    private void SetType(masterBallStruct thisball)
    {
        int myTypeNumber;
        switch (thisball.myTauntLevel)
        {
            case 0:
                thisball.myType = ballType.STANDARD;
                return;

            case 1:
                myTypeNumber = Random.Range(0, 3);
                if (myTypeNumber == 2)
                {
                    thisball.myType = ballType.ARC;
                }
                else
                {
                    thisball.myType = ballType.STANDARD;
                }
                return;

            case 2:
                myTypeNumber = Random.Range(0, 5);
                if (myTypeNumber == 2 || myTypeNumber == 3)
                {
                    thisball.myType = ballType.ARC;
                }
                else if (myTypeNumber == 4)
                {
                    thisball.myType = ballType.MULTI;
                }
                else
                {
                    thisball.myType = ballType.STANDARD;
                }
                return;

            case 3:
                myTypeNumber = Random.Range(0, 7);
                if (myTypeNumber == 2 || myTypeNumber == 3)
                {
                    thisball.myType = ballType.ARC;
                }
                else if (myTypeNumber == 4 || myTypeNumber == 5)
                {
                    thisball.myType = ballType.MULTI;
                }
                else if (myTypeNumber == 6)
                {
                    thisball.myType = ballType.SCATTER;
                }
                else
                {
                    thisball.myType = ballType.STANDARD;
                }
                return;

            default:
                myTypeNumber = Random.Range(0, 8);
                if (myTypeNumber == 2 || myTypeNumber == 3)
                {
                    thisball.myType = ballType.ARC;
                }
                else if (myTypeNumber == 4 || myTypeNumber == 5)
                {
                    thisball.myType = ballType.MULTI;
                }
                else if (myTypeNumber == 6 || myTypeNumber == 7)
                {
                    thisball.myType = ballType.SCATTER;
                }
                else
                {
                    thisball.myType = ballType.STANDARD;
                }
                return;
        }
    }

    private void StandardBallSetup(masterBallStruct thisball)
    {
        thisball.myThrowSpeed = 0.5f + (2 / thisball.myTauntLevel + 1);
        thisball.myReadySpeed = 0.5f + (2 / thisball.myTauntLevel + 1);
    }
    private void ArcBallSetup(masterBallStruct thisball)
    {
        thisball.myThrowSpeed = 0.7f + (2 / thisball.myTauntLevel + 1);
        thisball.myReadySpeed = 0.7f + (2 / thisball.myTauntLevel + 1);
    }
    private void MultiBallSetup(masterBallStruct thisball)
    {
        thisball.myThrowSpeed = 1f + (2 / thisball.myTauntLevel + 1);
        thisball.myReadySpeed = 0.7f + (2 / thisball.myTauntLevel + 1);
    }
    private void ScatterBallSetup(masterBallStruct thisball)
    {
        thisball.myThrowSpeed = 1.2f + (2 / thisball.myTauntLevel + 1);
        thisball.myReadySpeed = 0.3f + (2 / thisball.myTauntLevel + 1);
    }

    //That's it. Done. Everything after this is for actually throwing
}
