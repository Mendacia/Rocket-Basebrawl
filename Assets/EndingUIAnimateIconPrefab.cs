using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUIAnimateIconPrefab : MonoBehaviour
{
    private Vector3 defaultScale;
    [SerializeField] private Vector3 maxSize;
    [SerializeField] private float minSizeDifferencebeforeSnap;
    [SerializeField] private float lerpSpeed;
    private void Awake()
    {
        defaultScale = gameObject.transform.localScale;
        gameObject.transform.localScale = maxSize;
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.localScale, defaultScale) > minSizeDifferencebeforeSnap)
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, defaultScale, lerpSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.localScale = defaultScale;
            this.enabled = false;
        }
    }
}
