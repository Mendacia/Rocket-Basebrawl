using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Text setup for the UI")]
    [SerializeField] private Text unstableScore = null;
    [SerializeField] private Text combo = null;
    [SerializeField] private Text baseNumber = null;
    [SerializeField] private Transform arrowHolder = null;
    public Transform ballIconHolder;

    [SerializeField] private GameObject baseCanvas = null;
    [SerializeField] private Image currentBallIcon = null;
    [SerializeField] private Image nextBallIcon = null;
    [SerializeField] private GameObject comboObject = null;
    [SerializeField] private GameObject comboX = null;

    [Header("Prefab Setup")]
    public GameObject ballIconObject;
    [SerializeField] private GameObject arrowObject = null;

    private int targetUnstableScore;
    private int currentlyDisplayedUnstableScore;

    private void Start()
    {
        SetTheBaseString("1");
    }

    private void Update()
    {
        currentlyDisplayedUnstableScore = (int)Mathf.Lerp(currentlyDisplayedUnstableScore, targetUnstableScore, 1.5f * Time.deltaTime);
        if (currentlyDisplayedUnstableScore + 100 > targetUnstableScore)
        {
            currentlyDisplayedUnstableScore = targetUnstableScore;
        }
        unstableScore.text = currentlyDisplayedUnstableScore.ToString();
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
        currentBallIcon.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, currentTaunt);
        if (currentTaunt != 3) nextBallIcon.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, currentTaunt + 1);
        else nextBallIcon.sprite = BallIconHolder.GetIcon(BallResult.UNTHROWN, currentTaunt);

        Debug.Log("current ball should be " + currentTaunt);
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
