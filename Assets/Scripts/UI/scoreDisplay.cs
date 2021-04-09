using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreDisplay : MonoBehaviour
{
    [System.NonSerialized] public scoreHolder theAllSeeingScoreHolder;

    void Start()
    {
        theAllSeeingScoreHolder = GameObject.Find("DDOL_Scoreholder").GetComponent<scoreHolder>();
    }

    void Update()
    {
        gameObject.GetComponent<Text>().text = theAllSeeingScoreHolder.score.ToString();
    }
}
