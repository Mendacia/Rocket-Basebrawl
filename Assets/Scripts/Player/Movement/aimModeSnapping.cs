using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimModeSnapping : MonoBehaviour
{
    [SerializeField] private Transform from = null;
    public Transform to = null;
    [SerializeField] private float lerpDistance = 0.8f;
    [SerializeField] private bool timeDriven = true;

    private void Update()
    {
        gameObject.transform.position = Vector3.Lerp(from.position, new Vector3(to.position.x, to.position.y, from.position.z), lerpDistance * (timeDriven ? Time.deltaTime : 1));
    }
}
