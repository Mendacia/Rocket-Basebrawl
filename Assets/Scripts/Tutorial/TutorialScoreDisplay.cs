using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScoreDisplay : MonoBehaviour
{
    [System.NonSerialized] public scoreHolder theAllSeeingScoreHolder;

    void Start()
    {
        theAllSeeingScoreHolder = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
    }

    void Update()
    {
        gameObject.GetComponent<Text>().text = "Balls Hit: " + theAllSeeingScoreHolder.score + "/3";
    }
}
