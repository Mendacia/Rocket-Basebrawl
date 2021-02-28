using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffTutorial : MonoBehaviour
{
    void Start()
    {
        TutorialGodScript.isTutorial = false;
    }
}
