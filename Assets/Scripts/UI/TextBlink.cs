using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    private Text clickStart;
    private bool shouldFadeOut = true;
    private bool fadeOut = true;
    private bool fadeIn = true;
    private Color zeroAlpha;
    private Color fullAlpha;

    // Start is called before the first frame update
    void Start()
    {
        clickStart = this.gameObject.GetComponent<Text>();
        zeroAlpha.r = clickStart.color.r; zeroAlpha.g = clickStart.color.g; zeroAlpha.b = clickStart.color.b;
        zeroAlpha.a = 0;
        fullAlpha.r = clickStart.color.r; fullAlpha.g = clickStart.color.g; fullAlpha.b = clickStart.color.b;
        fullAlpha.a = 255;
    }

    // Update is called once per frame
    void Update()
    {
        if(clickStart.color.a == 0 && !fadeIn)
        {
            fadeIn = true;
            fadeOut = false;
            shouldFadeOut = false;
            StartCoroutine(FadeIn());
        }
        if(clickStart.color.a == 255 && !fadeOut)
        {
            fadeIn = false;
            fadeOut = true;
            shouldFadeOut = true;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        while (clickStart.color.a > 0 && shouldFadeOut)
        {
            clickStart.color = Color.Lerp(clickStart.color, zeroAlpha, 4 * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        while(clickStart.color.a < 255 && !shouldFadeOut)
        {
            clickStart.color = Color.Lerp(clickStart.color, fullAlpha, 4 * Time.deltaTime);
            yield return null;
        }
    }
}
