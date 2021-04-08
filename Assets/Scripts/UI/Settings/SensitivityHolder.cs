using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivityHolder : MonoBehaviour
{
    public static SensitivityHolder sensHold;
    public float sensitivity = 50;

private void Awake()
    {
        DontDestroyOnLoad(this);

        if (sensHold == null)
        {
            sensHold = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
