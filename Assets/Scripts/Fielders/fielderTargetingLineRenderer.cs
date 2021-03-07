using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingLineRenderer : MonoBehaviour
{
    [Header("Set this to the prefab that this script is on")]
    [SerializeField] private LineRenderer targetingBeam = null;
    [Header("This is roughly how fast the beam will shrink before the ball fires")]
    public float beamSizeDecreaseSpeed = 1f;
    [Header("This, in seconds, is how close to the ball firing you have to hit the ball to get gold")]
    public float goldThreshold = 0.2f;
    [Header("These are the visual controls for the linerenderer")]
    [SerializeField] private Color lineRendererColour;
    [SerializeField] private Color lineRendererColourEXPlusUltra;
    [SerializeField] private LayerMask hitterLayerMask;
    [SerializeField] private LayerMask physicsLayerMask;
    [SerializeField] private GameObject oSprite = null;
    [SerializeField] private GameObject xSprite = null;
    [SerializeField] private Gradient myGradient = new Gradient();
    private float currentBeamTime = 0;

    private float beamWidth = 1f;
    [System.NonSerialized] public Vector3 direction = Vector3.zero;
    [System.NonSerialized] public Vector3 originPosition = Vector3.zero;
    [System.NonSerialized] public Transform playerTransform = null;
    [System.NonSerialized] public bool theUIArrowScriptHasTheOsprite = false;
    [System.NonSerialized] public GameObject myArrow = null;
    [System.NonSerialized] public float beamTimeLimit = 1;
    [System.NonSerialized] public bool sweetSpotActive = false;

    private void Update()
    {
        var startPoint = originPosition;
        var midPoint = originPosition;
        var midPointTwo = originPosition;
        var endPoint = originPosition;
        //Get new hitter raycast hit
        if (Physics.Raycast(originPosition, direction, out var hitterRayCastHit, 1000, hitterLayerMask, QueryTriggerInteraction.Collide))
        {
            midPoint = hitterRayCastHit.point;
            fire(true, midPoint);
        }
        //Get new physics raycast hit
        if (Physics.Raycast(originPosition, direction, out var physicsRaycastHit, 1000, physicsLayerMask))
        {
            midPoint = startPoint + direction * Vector3.Distance(startPoint, playerTransform.position);
            midPointTwo = startPoint + (direction * 1.01f) * Vector3.Distance(startPoint, playerTransform.position);
            endPoint = physicsRaycastHit.point;
        }

        //Set points to original start point and raycast hit point
        var positions = new List<Vector3>();

        //Theoretically, go start-mid, then mid-end with 2 line renderers
        //midPointTwo exists for colouring purposes

        positions.Add(startPoint);
        positions.Add(midPoint);
        positions.Add(midPointTwo);
        positions.Add(endPoint);
        targetingBeam.positionCount = positions.Count;
        targetingBeam.SetPositions(positions.ToArray());
        oSprite.transform.position = midPoint;
        xSprite.transform.position = midPoint;
        myArrow.GetComponent<UIArrow>().giveTheUIArrowTheMidPoint(oSprite.transform);
        myArrow.GetComponent<UIArrow>().myColor = lineRendererColour;
        //Start shrinking that beam
        targetingBeam.startWidth = beamWidth;
        targetingBeam.endWidth = beamWidth;

        ShrinkBeam(midPoint);
        ColourBeam(startPoint, midPoint, endPoint);
    }

    public void GildMe()
    {
        goldThreshold = beamTimeLimit;
    }

    private void ShrinkBeam(Vector3 fireAt)
    {
        if (currentBeamTime < beamTimeLimit)
        {
            currentBeamTime = currentBeamTime + 1/beamSizeDecreaseSpeed * Time.deltaTime;
            beamWidth = 1 - (currentBeamTime / beamTimeLimit);
            oSprite.transform.localScale = new Vector3(beamWidth, beamWidth, beamWidth);

            if(currentBeamTime > beamTimeLimit - goldThreshold)
            {
                sweetSpotActive = true;
            }
        }
        else
        {
            fire(false, fireAt); 
        }
    }

    private void ColourBeam(Vector3 recievedStartPoint, Vector3 recievedMidPoint, Vector3 recievedEndPoint)
    {
        var midDistance = Vector3.Distance(recievedStartPoint, recievedMidPoint) / Vector3.Distance(recievedStartPoint, recievedEndPoint);
        myGradient.SetKeys(
             /*Colour Keys*/new GradientColorKey[] { new GradientColorKey(lineRendererColour, 0.0f), new GradientColorKey(lineRendererColour, midDistance), new GradientColorKey(lineRendererColourEXPlusUltra, (midDistance * 1.01f)), new GradientColorKey(lineRendererColourEXPlusUltra, 1) },
             /*Alpha Keys*/new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, midDistance), new GradientAlphaKey(0f, 1f) }
            );
        targetingBeam.colorGradient = myGradient;
    }

    private void fire(bool playerHitTheBall, Vector3 midPoint)
    {
        if (playerHitTheBall)
        {
        }
        else
        {
            gameObject.GetComponent<fielderTargetingBallSpawner>().SpawnTheBaseballPrefabAndThrowItAtTheTarget();
        }
        Destroy(myArrow);
        Destroy(gameObject);
    }
}
