using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stringToCaps : MonoBehaviour
{
    [SerializeField] private Text playerText;

    public void changeString(string myText)
    {
        playerText.text = myText.ToUpper();
    }
}
