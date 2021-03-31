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
        StartCoroutine(cameraMove());
    }

    IEnumerator cameraMove()
    {
        for (int t = 0; t < 5; t++)
        {
            if (rectX > 0.5f)
            {
                rectX = rectX - 0.1f;
            }
            myCamera.rect = new Rect(rectX, 0, 0.5f, 1);
            yield return new WaitForSeconds(0.0015f);
        }
        /*for (int t = 0; t < 9; t++)
        {
            if (rectX <= 0.5f)
            {
                rectX = rectX + 0.025f;
            }
            myCamera.rect = new Rect(rectX, 0, 0.5f, 1);
            yield return new WaitForSeconds(0.005f);
        }
        for (int t = 0; t < 19; t++)
        {
            if (rectX > 0.5f)
            {
                rectX = rectX - 0.01f;
            }
            myCamera.rect = new Rect(rectX, 0, 0.5f, 1);
            yield return new WaitForSeconds(0.005f);
        }*/
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 5; i++)
        {
            if (rectX < 1f)
            {
                rectX = rectX + 0.1f;
            }
            myCamera.rect = new Rect(rectX, 0, 0.5f, 1);
            yield return new WaitForSeconds(0.0015f);
        }
        this.gameObject.SetActive(false);
    }
}
