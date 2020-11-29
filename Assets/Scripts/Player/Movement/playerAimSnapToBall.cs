using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAimSnapToBall : MonoBehaviour
{
    [SerializeField] private Transform closestFielerTargetTransform;
    [SerializeField] private Transform player;
    [SerializeField] private float lerpDistance;
    [SerializeField] private bool lerpIsTimeDriven;

    public void LerpToFielderTarget()
    {
        player.position = Vector3.Lerp(player.position, closestFielerTargetTransform.position, lerpDistance * (lerpIsTimeDriven ? Time.deltaTime : 1));
    }
}
