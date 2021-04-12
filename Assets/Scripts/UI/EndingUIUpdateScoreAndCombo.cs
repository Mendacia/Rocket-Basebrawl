using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingUIUpdateScoreAndCombo : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text comboText;
    private void Awake()
    {
        scoreText.text = scoreHolder.scoreStatic.score.ToString();
        comboText.text = scoreHolder.scoreStatic.myCombo.ToString();
    }
}
