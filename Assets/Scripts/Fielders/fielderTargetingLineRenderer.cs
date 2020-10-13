using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingLineRenderer : MonoBehaviour
{
    [Header("Set this to the prefab that this script is on")]
    [SerializeField] private LineRenderer targetingBeam = null;
    [Header("Testing this for colour changes")]
    [SerializeField] private Color lineRendererColour;
    [SerializeField] private LayerMask hitterLayerMask;
    [SerializeField] private LayerMask physicsLayerMask;
    [SerializeField] private GameObject oSprite = null;
    [SerializeField] private GameObject xSprite = null;
    private float beamWidth = 1f;
    [System.NonSerialized] public Vector3 direction = Vector3.zero;
    [System.NonSerialized] public Vector3 originPosition = Vector3.zero;
    [System.NonSerialized] public Transform playerTransform = null;

    private void Update()
    {
        var startPoint = originPosition;
        var midPoint = originPosition;
        var endPoint = originPosition;
        var inHitterRadius = false;
        //Get new hitter raycast hit
        if (Physics.Raycast(originPosition, direction, out var hitterRayCastHit, 1000, hitterLayerMask, QueryTriggerInteraction.Collide))
        {
            midPoint = hitterRayCastHit.point;
            inHitterRadius = true;
        }
        //Get new physics raycast hit
        if (Physics.Raycast(originPosition, direction, out var physicsRaycastHit, 1000, physicsLayerMask))
        {
            endPoint = physicsRaycastHit.point;
        }

        //Set points to original start point and raycast hit point
        var positions = new List<Vector3>();

        //Theoretically, go start-mid, then mid-end with 2 line renderers

        positions.Add(startPoint);
        positions.Add(endPoint = startPoint + direction * Vector3.Distance(startPoint, playerTransform.position));
        targetingBeam.positionCount = positions.Count;
        targetingBeam.SetPositions(positions.ToArray());
        oSprite.transform.position = endPoint;
        xSprite.transform.position = endPoint;
        //Start shrinking that beam
        targetingBeam.startWidth = beamWidth;
        targetingBeam.endWidth = beamWidth;

        if (beamWidth > 0)
        {
            beamWidth = beamWidth - 1f *Time.deltaTime;
            oSprite.transform.localScale = new Vector3(beamWidth, beamWidth, beamWidth);
            targetingBeam.SetColors(lineRendererColour, lineRendererColour);
        }
        else
        {
            if (inHitterRadius)
            {
                gameObject.GetComponent<fielderTargetingSuccessfulHit>().SpawnTheBaseballPrefabAtThePlayerAndHitItRealHard(midPoint);
                Destroy(gameObject);
            }
            else
            {
                gameObject.GetComponent<fielderTargetingBallSpawner>().SpawnTheBaseballPrefabAndThrowItAtTheTarget();
                Destroy(gameObject);
            }
        }
    }
}
