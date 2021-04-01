﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingUIPopulateMedals : MonoBehaviour
{
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
    [SerializeField] private float tempCountGold, tempCountSilver, tempCountMissed;
    private int i = 0;
    private float waitTime = 0.1f;
    private int offset = 20;

    private enum passes
    {
        GOLD,
        SILVER,
        MISSED,
        ENDED
    }
    private passes currentPass = passes.GOLD;

    private void Awake()
    {
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
                currentPass = passes.ENDED;

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
        else if (currentPass == passes.ENDED)
        {
            goldTotalText.SetActive(true);
            silverTotalText.SetActive(true);
            missedTotalText.SetActive(true);

            goldTotalText.GetComponent<Text>().text = scoreHolder.scoreStatic.myGold.ToString();
            silverTotalText.GetComponent<Text>().text = scoreHolder.scoreStatic.mySilver.ToString();
            missedTotalText.GetComponent<Text>().text = scoreHolder.scoreStatic.myMiss.ToString();
        }
    }
    IEnumerator waitToContinue()
    {
        yield return new WaitForSeconds(waitTime);
        PopulateMedalList();
    }
}
