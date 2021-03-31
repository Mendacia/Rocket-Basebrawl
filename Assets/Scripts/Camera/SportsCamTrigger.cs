using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportsCamTrigger : MonoBehaviour
{
    [SerializeField] private int triggerFloat = 0;
    [SerializeField] private List<GameObject> sportsCams;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            sportsCams[0].SetActive(false);
            sportsCams[1].SetActive(false);
            sportsCams[2].SetActive(false);
            sportsCams[3].SetActive(false);
            sportsCams[triggerFloat].SetActive(true);
        }
    }
}
