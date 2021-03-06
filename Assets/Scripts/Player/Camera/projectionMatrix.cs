﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class projectionMatrix : MonoBehaviour
{
    public Matrix4x4 originalProjection;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalProjection = cam.projectionMatrix;
    }

    void Update()
    {
        Matrix4x4 p = originalProjection;
        p.m01 += Mathf.Cos(Time.time * 100.2F) * 0.1F;
        p.m10 += Mathf.Cos(Time.time * 100.5F) * 0.1F;
        cam.projectionMatrix = p;
    }
}
