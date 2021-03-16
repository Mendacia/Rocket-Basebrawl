using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Text setup for the UI")]
    [SerializeField] private Text unstableScore;
    [SerializeField] private Text combo;
    [SerializeField] private Text baseNumber;
    [SerializeField] private Transform arrowHolder;
    public Transform ballIconHolder;

    [Header("Prefab Setup")]
    public GameObject ballIconObject;
    [SerializeField] private GameObject arrowObject;

    private int targetUnstableScore;
    private int currentlyDisplayedUnstableScore;
    [SerializeField] private float lerp = 0f, duration = 2f;



    private void Update()
    {
        lerp += Time.deltaTime / duration;
        currentlyDisplayedUnstableScore = (int)Mathf.Lerp(currentlyDisplayedUnstableScore, targetUnstableScore, lerp);
        unstableScore.text = currentlyDisplayedUnstableScore.ToString();
    }

    public void SetTheTargetUnstableScore(int recievedScore) => targetUnstableScore = recievedScore;

    public void SetTheBaseString(string recievedBase)
    {
        baseNumber.text = recievedBase;
    }

    public void UpdateTheComboMultiplier(int recievedCombo)
    {
        combo.text = recievedCombo.ToString();
    }
}
