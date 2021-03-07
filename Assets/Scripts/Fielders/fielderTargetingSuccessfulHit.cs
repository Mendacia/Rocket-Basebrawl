using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingSuccessfulHit : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;

    private BaseballEffectHolder effectHolder;
    private scoreUpdater myScoreUpdater;
    private scoreHolder myScoreHolder;
    private AudioSource pitchChange;

    private void Start()
    {
        effectHolder = GameObject.Find("BaseballEffectHolder").GetComponent<BaseballEffectHolder>();
        myScoreUpdater = GameObject.Find("ScoreUpdater").GetComponent<scoreUpdater>();
        myScoreHolder = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
        pitchChange = Camera.main.GetComponent<AudioSource>();
    }
}