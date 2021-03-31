using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallList : MonoBehaviour
{
    public List<masterBallStruct> masterBallList;
    [SerializeField] private HUDManager hUDScript;
    [SerializeField] private float readyAndThrowSpeedMultiplier = 1f;

    //Between this and the next comment is entirely setup of variables for the ball list
    public void AddThisBallToTheList(int Index, int taunt, Transform fielders)
    {
        masterBallStruct thisBall = new masterBallStruct();
        thisBall.myIndex = Index;
        thisBall.myTauntLevel = taunt;
        thisBall.myFielders = new List<Transform>();
        thisBall = AssignRemainingVariables(thisBall);
        thisBall = hUDScript.addBallToTheUI(thisBall);
        masterBallList.Add(thisBall);
    }

    public void AddFieldersToTheBall(List<Transform> fielders, int index)
    {
        var thisBall = masterBallList[index];
        foreach(Transform fielder in fielders)
        {
            thisBall.myFielders.Add(fielder);
        }
        masterBallList[index] = thisBall;
    }

    public masterBallStruct AssignRemainingVariables(masterBallStruct ball)
    {
        ball.myType = SetType(ball).myType;
        switch (ball.myType)
            {
                case ballType.STANDARD:
                    ball = StandardBallSetup(ball);
                    return ball;

                case ballType.ARC:
                    ball = ArcBallSetup(ball);
                    return ball;

                case ballType.MULTI:
                    ball = MultiBallSetup(ball);
                    return ball;

                case ballType.SCATTER:
                    ball = ScatterBallSetup(ball);
                    return ball;

                default:
                    ball = StandardBallSetup(ball);
                    return ball;
            }
    }

    private masterBallStruct SetType(masterBallStruct thisball)
    {
        int myTypeNumber;
        switch (thisball.myTauntLevel)
        {
            case 0:
                thisball.myType = ballType.STANDARD;
                return thisball;

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
                return thisball;

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
                return thisball;

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
                return thisball;

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
                return thisball;
        }
    }

    private masterBallStruct StandardBallSetup(masterBallStruct thisball)
    {
        thisball.myThrowSpeed = (0.5f + (2 / (thisball.myTauntLevel + 1) * Random.Range(0.7f, 1.1f))) * readyAndThrowSpeedMultiplier;
        thisball.myReadySpeed = (0.5f + (2 / (thisball.myTauntLevel + 1) * Random.Range(0.7f, 1.1f))) * readyAndThrowSpeedMultiplier;
        return thisball;
    }
    private masterBallStruct ArcBallSetup(masterBallStruct thisball)
    {
        thisball.myThrowSpeed = (0.7f + (2 / thisball.myTauntLevel + 1) * Random.Range(0.7f, 1.1f)) * readyAndThrowSpeedMultiplier;
        thisball.myReadySpeed = (0.7f + (2 / thisball.myTauntLevel + 1) * Random.Range(0.7f, 1.1f)) * readyAndThrowSpeedMultiplier;
        return thisball;
    }
    private masterBallStruct MultiBallSetup(masterBallStruct thisball)
    {
        thisball.myThrowSpeed = (1f + (2 / thisball.myTauntLevel + 1) * Random.Range(0.7f, 1.1f)) * readyAndThrowSpeedMultiplier;
        thisball.myReadySpeed = (0.7f + (2 / thisball.myTauntLevel + 1) * Random.Range(0.7f, 1.1f)) * readyAndThrowSpeedMultiplier;
        thisball.extraBallCount = Random.Range(1, 4);
        return thisball;
    }
    private masterBallStruct ScatterBallSetup(masterBallStruct thisball)
    {
        thisball.myThrowSpeed = (1.2f + (2 / thisball.myTauntLevel + 1) * Random.Range(0.7f, 1.1f))* readyAndThrowSpeedMultiplier;
        thisball.myReadySpeed = (0.3f + (2 / thisball.myTauntLevel + 1) * Random.Range(0.7f, 1.1f)) * readyAndThrowSpeedMultiplier;
        thisball.extraBallCount = Random.Range(5, 10);
        return thisball;
    }

    //That's it. Done. Everything after this is for actually throwing

    //I need to set up both the current ball and the next ball, as the time it takes for the pitcher to request the next ball is dependant on ball 2's 'myReadySpeed'

    public masterBallStruct CallForBall()
    {
        for (int i = 0; i < masterBallList.Count; i++)
        {
            var testedBall = masterBallList[i];
            if (testedBall.currentState == ballState.INACTIVE)
            {
                Debug.Log("Fired a ball with itterator at " + i);
                testedBall.currentState = ballState.ACTIVE;
                masterBallList[i] = testedBall;
                return testedBall;
            }
            else if (i + 1 == masterBallList.Count)
            {
            }
        }
        return new masterBallStruct() { myIndex = -1 };
    }

    public void SetToGold(int recievedIndex)
    {
        Debug.Log("Set To Gold");
        var thisBall = masterBallList[recievedIndex];
        thisBall.currentState = ballState.GOLD;
        thisBall =  hUDScript.changeBallUISpriteToCorrectColor(thisBall, BallResult.GOLD);
        masterBallList[recievedIndex] = thisBall;
    }
    public void SetToSilver(int recievedIndex)
    {
        Debug.Log("Set To Silver");
        var thisBall = masterBallList[recievedIndex];
        thisBall.currentState = ballState.SILVER;
        thisBall = hUDScript.changeBallUISpriteToCorrectColor(thisBall, BallResult.SILVER);
        masterBallList[recievedIndex] = thisBall;
    }
    public void SetToMiss(int recievedIndex)
    {
        Debug.Log("Set To Miss");
        var thisBall = masterBallList[recievedIndex];
        thisBall.currentState = ballState.MISSED;
        thisBall = hUDScript.changeBallUISpriteToCorrectColor(thisBall, BallResult.MISS);
        masterBallList[recievedIndex] = thisBall;
    }
}

[System.Serializable]
public struct masterBallStruct
{
    public int myIndex;                      //This ball's place in both this script's List and fielderPeltingScript's List
    public ballType myType;               //Type of throw the fielders will use when firing this ball
    public ballState currentState;         //Mostly used for UI stuff but also tells the ball when it's active
    public int myTauntLevel;               //The level of taunt assigned to this ball. Manages other variables
    public GameObject uIObject;          //The object on the UI that handles the sprite
    public List <Transform> myFielders; //The fielder that will throw this ball
    public float myThrowSpeed;          //Speed at which beam decreases in size
    public float myReadySpeed;          //Time between last ball and this ball
    public int extraBallCount;              //Extra balls for multi and scatter
}

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
