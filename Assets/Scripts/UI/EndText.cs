using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndText : MonoBehaviour
{
    [SerializeField] private List<string> positiveText;
    [SerializeField] private List<string> negativeText;
    private scoreHolder scoreHolderScript;
    private Text myText;
    void Start()
    {
        myText = gameObject.GetComponent<Text>();
        scoreHolderScript = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();

        if(scoreHolderScript.score == 0)
        {
            myText.text = ("Stop trying to break the game. You got 0 points. It's a draw.");
        }
        else if (scoreHolderScript.score > 0 && scoreHolderScript.score != 69)
        {
            myText.text = positiveText[Random.Range(0, positiveText.Count)];
        }
        else if (scoreHolderScript.score == 69)
        {
            myText.text = ("Nice");
        }
        else
        {
            myText.text = negativeText[Random.Range(0, negativeText.Count)];
        }
        
    }
}
