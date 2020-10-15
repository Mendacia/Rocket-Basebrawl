using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreHolder : MonoBehaviour
{
    [Header("Set this to the UI score display")]
    [SerializeField] private Text scoreText = null;
    [System.NonSerialized]public int score = 0;

    private void Update()
    {
        scoreText.text = "Score : " + score;
    }
}
