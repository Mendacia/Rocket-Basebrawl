using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScoreOnAwake : MonoBehaviour
{
    private void Awake()
    {
        GameObject.Find("Scoreholder").GetComponent<scoreHolder>().score = 0;
    }
}
