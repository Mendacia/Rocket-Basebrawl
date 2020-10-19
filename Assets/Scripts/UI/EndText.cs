using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndText : MonoBehaviour
{
    [SerializeField] private List<string> displayText;
    private Text myText;
    void Start()
    {
        myText = gameObject.GetComponent<Text>();
        myText.text =  displayText[Random.Range(0, displayText.Count)];
    }
}
