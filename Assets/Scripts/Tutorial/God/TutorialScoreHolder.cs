using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScoreHolder : MonoBehaviour
{
    [System.NonSerialized] public float score = 0;

    void Start()
    {
        score = 0;
    }
}
