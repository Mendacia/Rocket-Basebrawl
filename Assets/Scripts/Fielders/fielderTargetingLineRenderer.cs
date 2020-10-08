using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingLineRenderer : MonoBehaviour
{
    [Header("Set this to the prefab that this script is on")]
    [SerializeField] private LineRenderer targetingBeam = null;
    [Header("Testing this for colour changes")]
    [SerializeField] private Color lineRendererColour;
    private float beamWidth = 1f;
    [System.NonSerialized] public Vector3 fielderRaycastHitPosition = Vector3.zero;
    [System.NonSerialized] public Transform fielderPosition = null;

    //These arguments are set in fielderPeltingScript
    public void ArmTheLineRenderer()
    {
        //Now we need to make a list of transforms to then give the line renderer This needs to be here so I can ensure in PeltingScript that the variables are set before running the code
        var positions = new List<Vector3>();
        positions.Add(fielderPosition.position);
        positions.Add(fielderRaycastHitPosition);
        targetingBeam.positionCount = positions.Count;
        targetingBeam.SetPositions(positions.ToArray());
    }

    private void Update()
    {
        //Start shrinking that beam
        targetingBeam.startWidth = beamWidth;
        targetingBeam.endWidth = beamWidth;
        if (beamWidth > 0)
        {
            beamWidth = beamWidth - 0.002f;
            targetingBeam.SetColors(lineRendererColour, lineRendererColour);
        }
        else
        {
            gameObject.GetComponent<fielderTargetingBallSpawner>().SpawnTheBaseballPrefabAndThrowItAtTheTarget();
            Destroy(gameObject);
        }
    }
}
