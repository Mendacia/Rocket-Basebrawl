using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArrow : MonoBehaviour
{
    [SerializeField] private Transform pitcher;
    private void Awake()
    {
        pitcher = GameObject.Find("Pitcher").transform;
    }


    private void Update()
    {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(pitcher.position);
        Debug.Log("I'm trying to go to X" + Camera.main.WorldToScreenPoint(pitcher.position).x + " Y" + Camera.main.WorldToScreenPoint(pitcher.position).y);
    }
}
