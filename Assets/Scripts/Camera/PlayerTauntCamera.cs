using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTauntCamera : MonoBehaviour
{
    private Camera myCamera;
    private float rectX = 1;

    void Awake()
    {
        myCamera = this.gameObject.GetComponent<Camera>();
    }

    void Start()
    {
        myCamera.rect = new Rect(1, 0, 0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(rectX > 0.5f)
        {
            rectX = rectX - 0.05f;
        }
        myCamera.rect = new Rect(rectX, 0, 0.5f, 1);
    }
}
