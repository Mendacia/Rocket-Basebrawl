﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelFollow : MonoBehaviour
{
    public Transform player;
    public Transform camRot;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        //transform.eulerAngles = new Vector3(0, player.eulerAngles.y, 0);
        transform.eulerAngles = new Vector3(0, camRot.eulerAngles.y, 0);
    }
}
