using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveateCamera : MonoBehaviour
{
    public GameObject startCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            startCam.SetActive(false);
        }
    }
}
