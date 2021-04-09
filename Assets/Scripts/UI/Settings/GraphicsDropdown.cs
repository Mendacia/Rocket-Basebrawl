using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsDropdown : MonoBehaviour
{
    [SerializeField] private Text myDescription = null;

    private void Awake()
    {
        myDescription.text = "Very Low";
    }

    public void SetQuality(Slider mySlider)
    {
        QualitySettings.SetQualityLevel((int)mySlider.value, true);
        Debug.Log("I am outputting " + mySlider.value);

        switch (mySlider.value)
        {
            default:
                myDescription.text = "Very Low";
                break;
            case 1:
                myDescription.text = "Low";
                break;
            case 2:
                myDescription.text = "Medium";
                break;
            case 3:
                myDescription.text = "High";
                break;
            case 4:
                myDescription.text = "Very High";
                break;
            case 5:
                myDescription.text = "Intended";
                break;
        }
    }
}