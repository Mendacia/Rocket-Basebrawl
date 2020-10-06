using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DeactiveateCamera : MonoBehaviour
{
    public GameObject startCam;
    public CinemachineDollyCart dolCart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(dolSpeed());
        }
    }
    
    public IEnumerator dolSpeed()
    {
        dolCart.m_Speed = 10000;
        yield return new WaitForSeconds(0.5f);
        startCam.SetActive(false);
    }
}
