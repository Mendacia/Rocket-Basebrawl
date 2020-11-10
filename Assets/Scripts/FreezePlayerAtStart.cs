using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePlayerAtStart : MonoBehaviour
{
    [SerializeField] private playerControls playerStateReference;

    private void Awake()
    {
        playerStateReference.playerState = 0;
        playerStateReference.isFrozen = true;
    }
}
