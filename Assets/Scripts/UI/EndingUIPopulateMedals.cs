using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingUIPopulateMedals : MonoBehaviour
{
    [SerializeField] private SavingScript save;

    [Header("Assets/Prefabs/BallUIPostGame")]
    [SerializeField] private GameObject goldBallPrefab;
    [SerializeField] private GameObject silverBallPrefab;
    [SerializeField] private GameObject missedBallPrefab;

    [Header("I could populate this automatically but I can't guarantee that would work")]
    [SerializeField] private Transform goldHolderChild;
    [SerializeField] private Transform silverHolderChild;
    [SerializeField] private Transform missedHolderChild;

    [Header("Similar to above")]
    [SerializeField] private GameObject goldTotalText;
    [SerializeField] private GameObject silverTotalText;
    [SerializeField] private GameObject missedTotalText;

    [Header("Animation stuff")]
    [SerializeField] private float tempCountGold;
    [SerializeField] private float tempCountSilver;
    [SerializeField] private float tempCountMissed;

    [Header("Animating Score and Combo bits")]
    [SerializeField] private Text scoreDisplay;
    [SerializeField] private Text comboDisplay;
    [SerializeField] private GameObject returnButton;

    private bool scoreUpdateTime = false;
    private bool comboUpdateTime = false;
    private int displayedScore = 0;
    private int displayedCombo = 0;
    private int i = 0;
    private int scoreTimeMultiplier = 1;
    private float waitTime = 0.1f;
    private int offset = 20;
    private int totalGolds = 0;

    private enum passes
    {
        GOLD,
        SILVER,
        MISSED,
        SCORE,
        COMBO,
        IDLE
    }
    private passes currentPass = passes.GOLD;

    private void Awake()
    {
        save.LoadFromFile();
        StartCoroutine(delayMedals());
    }

    IEnumerator delayMedals()
    {
        yield return new WaitForSeconds(1);
        PopulateMedalList();
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            waitTime = 0.01f;
        }
        else
        {
            waitTime = 0.1f;
        }

        if (scoreUpdateTime)
        {
            if (displayedScore + 30 < scoreHolder.scoreStatic.score)
            {
                displayedScore += 30 * scoreTimeMultiplier;
            }
            else
            {
                displayedScore = (int)scoreHolder.scoreStatic.score;
                currentPass = passes.COMBO;
                scoreUpdateTime = false;
                Debug.Log("We should be in combo now");
                PopulateMedalList();
            }
            scoreDisplay.text = displayedScore.ToString();
        }

        if (comboUpdateTime)
        {
            Debug.Log("The combo should update to " + scoreHolder.scoreStatic.myCombo);
            if (displayedCombo + 1 < scoreHolder.scoreStatic.myCombo)
            {
                displayedCombo += 1;
            }
            else
            {
                displayedCombo = scoreHolder.scoreStatic.myCombo;
                currentPass = passes.IDLE;
                comboUpdateTime = false;
                PopulateMedalList();
            }
            comboDisplay.text = displayedCombo.ToString();
        }
    }

    public void PopulateMedalList()
    {
        if (i < ExportableBallList.instance.holdingList.Count && currentPass == passes.GOLD)
        {
            if (ExportableBallList.instance.holdingList[i].currentState == ballState.GOLD)
            {
                var thisPrefab = Instantiate(goldBallPrefab, goldHolderChild);
                thisPrefab.transform.position += new Vector3(Random.Range(-5, 5), offset, 0);
                thisPrefab.GetComponentInChildren<Image>().sprite = BallIconHolder.GetIcon(BallResult.GOLD, ExportableBallList.instance.holdingList[i].myTauntLevel);
                offset += 20;
                totalGolds++;
            }

            if(i == ExportableBallList.instance.holdingList.Count - 1)
            {
                offset = 20;
                i = 0;
                currentPass = passes.SILVER;

                if (ExportableBallList.instance.holdingList[i].currentState == ballState.GOLD)
                {
                    StartCoroutine(waitToContinue());
                }
                else
                {
                    PopulateMedalList();
                }
            }
            else
            {
                i++;
                if (ExportableBallList.instance.holdingList[i].currentState == ballState.GOLD)
                {
                    StartCoroutine(waitToContinue());
                }
                else
                {
                    PopulateMedalList();
                }
            }
        }
        else if (i < ExportableBallList.instance.holdingList.Count && currentPass == passes.SILVER)
        {
            if (ExportableBallList.instance.holdingList[i].currentState == ballState.SILVER)
            {
                var thisPrefab = Instantiate(silverBallPrefab, silverHolderChild);
                thisPrefab.transform.position += new Vector3(Random.Range(-5, 5), offset, 0);
                thisPrefab.GetComponentInChildren<Image>().sprite = BallIconHolder.GetIcon(BallResult.SILVER, ExportableBallList.instance.holdingList[i].myTauntLevel);
                offset += 20;
            }

            if (i == ExportableBallList.instance.holdingList.Count - 1)
            {
                offset = 20;
                i = 0;
                currentPass = passes.MISSED;

                if (ExportableBallList.instance.holdingList[i].currentState == ballState.SILVER)
                {
                    StartCoroutine(waitToContinue());
                }
                else
                {
                    PopulateMedalList();
                }
            }
            else
            {
                i++;
                if (ExportableBallList.instance.holdingList[i].currentState == ballState.SILVER)
                {
                    StartCoroutine(waitToContinue());
                }
                else
                {
                    PopulateMedalList();
                }
            }
        }
        else if (i < ExportableBallList.instance.holdingList.Count && currentPass == passes.MISSED)
        {
            if (ExportableBallList.instance.holdingList[i].currentState == ballState.MISSED)
            {
                var thisPrefab = Instantiate(missedBallPrefab, missedHolderChild);
                thisPrefab.transform.position += new Vector3(Random.Range(-5, 5), offset, 0);
                thisPrefab.GetComponentInChildren<Image>().sprite = BallIconHolder.GetIcon(BallResult.MISS, ExportableBallList.instance.holdingList[i].myTauntLevel);
                offset += 20;
            }

            if (i == ExportableBallList.instance.holdingList.Count - 1)
            {
                offset = 20;
                i = 0;
                currentPass = passes.SCORE;

                if (ExportableBallList.instance.holdingList[i].currentState == ballState.MISSED)
                {
                    StartCoroutine(waitToContinue());
                }
                else
                {
                    PopulateMedalList();
                }
            }
            else
            {
                i++;
                if (ExportableBallList.instance.holdingList[i].currentState == ballState.MISSED)
                {
                    StartCoroutine(waitToContinue());
                }
                else
                {
                    PopulateMedalList();
                }
            }
        }
        else if (currentPass == passes.SCORE)
        {
            goldTotalText.SetActive(true);
            silverTotalText.SetActive(true);
            missedTotalText.SetActive(true);

            goldTotalText.GetComponent<Text>().text = scoreHolder.scoreStatic.myGold.ToString();
            silverTotalText.GetComponent<Text>().text = scoreHolder.scoreStatic.mySilver.ToString();
            missedTotalText.GetComponent<Text>().text = scoreHolder.scoreStatic.myMiss.ToString();

            scoreUpdateTime = true;
            StartCoroutine(scoreTimerIncreaser());
        }
        else if (currentPass == passes.COMBO)
        {
            comboUpdateTime = true;
        }
        else if (currentPass == passes.IDLE)
        {

            //Saving Right here.
            if(scoreHolder.scoreStatic.score > save.loadInt("score", 0))
            {
                save.SaveInt((int)scoreHolder.scoreStatic.score, "score");
            }

            if (scoreHolder.scoreStatic.myCombo > save.loadInt("combo", 0))
            {
                save.SaveInt((int)scoreHolder.scoreStatic.myCombo, "combo");
            }

            if (totalGolds > save.loadInt("golds", 0))
            {
                save.SaveInt((int)totalGolds, "golds");
            }
            save.SaveToFile();
            Destroy(ExportableBallList.instance.gameObject);
            returnButton.SetActive(true);
        }
    }
    IEnumerator waitToContinue()
    {
        yield return new WaitForSeconds(waitTime);
        PopulateMedalList();
    }

    IEnumerator scoreTimerIncreaser()
    {
        yield return new WaitForSeconds(2);
        scoreTimeMultiplier *= 2;
        if (currentPass == passes.SCORE)
        {
            StartCoroutine(scoreTimerIncreaser());
        }
    }
}
