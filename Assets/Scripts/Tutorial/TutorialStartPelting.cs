using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartPelting : MonoBehaviour
{
    [SerializeField] private fielderPeltingScript peltingReference = null;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)){
            peltingReference.startPeltingLoop();
        }
    }
}
