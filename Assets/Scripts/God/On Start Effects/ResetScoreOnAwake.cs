using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScoreOnAwake : MonoBehaviour
{
    private void Awake()
    {
        GameObject.Find("DDOL_Scoreholder").GetComponent<scoreHolder>().score = 0;
    }
}
