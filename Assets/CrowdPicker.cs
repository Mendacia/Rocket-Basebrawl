using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPicker : MonoBehaviour
{
    [SerializeField] private List<Sprite> crowdImages;
    [SerializeField] private SpriteRenderer myCrowd;

    void Awake()
    {
        var chosen = Random.Range(0, crowdImages.Count);
        myCrowd.sprite = crowdImages[chosen];
    }
}
