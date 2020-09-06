using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattingOnClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            HitBall();
        }
    }

    void HitBall()
    {
        //playAnimation
        if(Detector.isHittable == true)
        {
            //hitball
        }
    }
}
