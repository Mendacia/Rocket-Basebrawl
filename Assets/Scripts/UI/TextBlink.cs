using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    [SerializeField] private Text clickStart = null;
    private bool shouldFadeOut = true;
    private bool fadeOut = true;
    private bool fadeIn = false;
    private Color zeroAlpha;
    private Color fullAlpha;

    // Start is called before the first frame update
    void Start()
    {
        zeroAlpha = new Color(clickStart.color.r, clickStart.color.g, clickStart.color.b, 0);
        fullAlpha = new Color(clickStart.color.r, clickStart.color.g, clickStart.color.b, 1);
        shouldFadeOut = true;
        //clickStart.color = zeroAlpha;
        StartCoroutine(FadeOut());
    }

    private void Update()
    {
        if(clickStart.color.a <= 0.1f && !fadeIn)
        {
            fadeIn = true;
            fadeOut = false;
            shouldFadeOut = false;
            StopCoroutine(FadeOut());
            StartCoroutine(FadeIn());
        }
        if (clickStart.color.a >= 0.9 && !fadeOut)
        {
            fadeOut = true;
            fadeIn = false;
            shouldFadeOut = true;
            StopCoroutine(FadeIn());
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        while (clickStart.color.a > 0 && shouldFadeOut)
        {
            clickStart.color = Color.Lerp(clickStart.color, zeroAlpha, 1.5f * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        while(clickStart.color.a < 1 && shouldFadeOut == false)
        {
            clickStart.color = Color.Lerp(clickStart.color, fullAlpha, 1.5f * Time.deltaTime);
            yield return null;
        }
    }
}
