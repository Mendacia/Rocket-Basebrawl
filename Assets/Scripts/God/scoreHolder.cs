using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreHolder : MonoBehaviour
{
    [Header("Set this to the UI score display")]
    [System.NonSerialized]public int score = 0;

    private static scoreHolder scoreStatic;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (scoreStatic == null)
        {
            scoreStatic = this;
        }
        else
        {
            Object.Destroy(gameObject);
        }
    }
}
