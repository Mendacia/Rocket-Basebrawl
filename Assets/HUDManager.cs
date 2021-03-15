using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Text setup for the UI")]
    [SerializeField] private Text unstableScore;
    [SerializeField] private Text combo;
    [SerializeField] private Text baseNumber;
    [SerializeField] private Transform arrowHolder;
    [SerializeField] private Transform ballIconHolder;

    [Header("Prefab Setup")]
    [SerializeField] private GameObject ballIconObject;
    [SerializeField] private GameObject arrowObject;


    
}
