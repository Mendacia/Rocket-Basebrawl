using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Text setup for the UI")]
    [SerializeField] private Text unstableScore = null;
    [SerializeField] private Text score = null;
    [SerializeField] private Text combo = null;
    [SerializeField] private Text baseNumber = null;
    public Transform ballIconHolder;

    [SerializeField] private GameObject baseCanvas = null;
    private Animator baseCanvasAnimator = null;
    [SerializeField] private Image currentBallIcon = null;
    [SerializeField] private Image nextBallIcon = null;
    [SerializeField] private GameObject comboObject = null;
    [SerializeField] private GameObject comboX = null;

    [Header("Prefab Setup")]
    public GameObject ballIconObject;

    private int targetUnstableScore;
    private int currentlyDisplayedUnstableScore;
    private bool banking = false;

    [Header("These go to things on the BaseCanvas")]
    [SerializeField] private int defaultScoreAnimationFactor = 110;
    [SerializeField] private baseManager baseScript = null;
    [SerializeField] private Text baseUnstableScore = null;
    [SerializeField] private Text baseScore = null;
    private int displayedUnstableScore = 0;
    private int displayedScore = 0;
    private int targetDisplayedScore;
    private int scoreAnimationFactor;
    [Header("These are specifically for Taunt on BaseCanvas")]
    [SerializeField, TextArea] private string taunt0Text, taunt1Text, taunt2Text, taunt3PlusText;
    [SerializeField] private Image currentTauntBall, nextTauntBall;
    [SerializeField] private Text description;

    private void Start()
    {
        SetTheBaseString("1");
        baseCanvasAnimator = baseCanvas.GetComponent<Animator>();
    }

    private void Update()
    {
        currentlyDisplayedUnstableScore = (int)Mathf.Lerp(currentlyDisplayedUnstableScore, targetUnstableScore, 1.5f * Time.deltaTime);
        if (currentlyDisplayedUnstableScore + 100 > targetUnstableScore)
        {
            currentlyDisplayedUnstableScore = targetUnstableScore;
        }
        unstableScore.text = currentlyDisplayedUnstableScore.ToString();

        //This is purely for visuals on baseUI. It sucks, I hate it, but we have to deal with it
        if (banking)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                scoreAnimationFactor = defaultScoreAnimationFactor * 10;
            }
            else
            {
                scoreAnimationFactor = defaultScoreAnimationFactor;
            }

            if (targetDisplayedScore - displayedScore > scoreAnimationFactor)
            {
                displayedScore += scoreAnimationFactor;
                displayedUnstableScore -= scoreAnimationFactor;
                baseScore.text = displayedScore.ToString();
                unstableScore.text = displayedUnstableScore.ToString();
                score.text = displayedScore.ToString();
                baseUnstableScore.text = displayedUnstableScore.ToString();
            }
            else if (targetDisplayedScore - displayedScore > 0)
            {
                displayedScore = targetDisplayedScore;
                displayedUnstableScore = 0;
                baseScore.text = displayedScore.ToString();
                unstableScore.text = displayedUnstableScore.ToString();
                score.text = displayedScore.ToString();
                baseUnstableScore.text = displayedUnstableScore.ToString();
                targetUnstableScore = 0;
                banking = false;
                StartCoroutine(waitforBankingEnd());
            }
            else
            {
                displayedScore = targetDisplayedScore;
                displayedUnstableScore = 0;
                baseScore.text = displayedScore.ToString();
                unstableScore.text = displayedUnstableScore.ToString();
                score.text = displayedScore.ToString();
                baseUnstableScore.text = displayedUnstableScore.ToString();
                targetUnstableScore = 0;
                banking = false;
                StartCoroutine(waitforBankingEnd());
            }
        }
    }

    IEnumerator waitforBankingEnd()
    {
        baseCanvasAnimator.SetTrigger("NumbersFinished");
        yield return new WaitForSeconds(2);
        baseCanvasAnimator.SetTrigger("BankComplete");
        yield return new WaitForSeconds(0.42f);
        baseScript.EndBankAnimation();
    }

    public void SetTheTargetUnstableScore(int recievedScore) => targetUnstableScore = recievedScore;

    public void SetTheBaseString(string recievedBase) => baseNumber.text = recievedBase;

    public void UpdateTheComboMultiplier(int recievedCombo) => combo.text = recievedCombo.ToString();

    public masterBallStruct addBallToTheUI(masterBallStruct ball)
    {
        ball.uIObject = Instantiate(ballIconObject, ballIconHolder);
        ball.uIObject.GetComponentInChildren<Image>().sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, ball.myTauntLevel);
        return ball;
    }
    public masterBallStruct changeBallUISpriteToCorrectColor(masterBallStruct ball, BallResult myResult)
    {
        ball.uIObject.GetComponentInChildren<Image>().sprite = BallIconHolder.GetIcon(myResult, ball.myTauntLevel);
        return ball;
    }
    public void clearTheBallUI()
    {
        foreach(Transform child in ballIconHolder)
        {
            Destroy(child.gameObject);
        }
    }

    public void runTheBaseUI(bool isActive, int currentTaunt)
    {
        baseCanvas.SetActive(isActive);
        if (currentTaunt < 4) currentBallIcon.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, currentTaunt);
        else currentBallIcon.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, 3);

        if (currentTaunt < 3) nextBallIcon.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, currentTaunt + 1);
        else nextBallIcon.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, 3);

        Debug.Log("current ball should be " + currentTaunt);
    }

    public void baseUITaunt(int tauntLevelCurrent)
    {
        if (tauntLevelCurrent < 4) { currentTauntBall.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, tauntLevelCurrent); }
        else {}
        if (tauntLevelCurrent < 3) { nextTauntBall.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, tauntLevelCurrent + 1); }
        else {}

        switch (tauntLevelCurrent)
        {
            case 0:
                description.text = taunt0Text;
                break;
            case 1:
                description.text = taunt1Text;
                break;
            case 2:
                description.text = taunt2Text;
                break;
            default:
                description.text = taunt3PlusText;
                break;
        }

        baseCanvasAnimator.SetTrigger("Taunt");
    }
    public void baseUIHold()
    {
        baseCanvasAnimator.SetTrigger("Hold");
    }
    public void baseUIBank()
    {

        baseCanvasAnimator.SetTrigger("Bank");
        displayedScore = (int)scoreHolder.scoreStatic.score;
        displayedUnstableScore = targetUnstableScore;

        targetDisplayedScore = displayedScore + displayedUnstableScore;

        baseUnstableScore.text = displayedUnstableScore.ToString();
        baseScore.text = displayedScore.ToString();
        banking = true;
    }

    public void displayTheComboElement(Vector3 targetLocation)
    {
        combo.enabled = true;
        var myImage = comboObject.GetComponent<Image>().sprite;
        comboObject.transform.position = Camera.main.WorldToScreenPoint(targetLocation);
        comboX.transform.position = Camera.main.WorldToScreenPoint(targetLocation);
        comboObject.GetComponent<Animator>().SetTrigger("Display");
        comboX.GetComponent<Image>().enabled = true;
        comboX.GetComponent<Animator>().SetTrigger("Hit");
    }
}
