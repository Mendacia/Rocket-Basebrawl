using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingLineRenderer : MonoBehaviour
{
    [Header("Set this to the prefab that this script is on")]
    [SerializeField] private LineRenderer targetingBeam = null;
    [Header("These are the visual controls for the linerenderer")]
    [SerializeField] private Color lineRendererColour;
    [SerializeField] private Color lineRendererColourEXPlusUltra;
    [SerializeField] private LayerMask hitterLayerMask;
    [SerializeField] private LayerMask physicsLayerMask;
    [SerializeField] private GameObject oSprite = null;
    [SerializeField] private GameObject xSprite = null;
    [SerializeField] private Gradient myGradient = new Gradient();

    private float beamWidth = 1f;
    [System.NonSerialized] public Vector3 direction = Vector3.zero;
    [System.NonSerialized] public Vector3 originPosition = Vector3.zero;
    [System.NonSerialized] public Transform playerTransform = null;
    [System.NonSerialized] public bool theUIArrowScriptHasTheOsprite = false;
    [System.NonSerialized] public GameObject myArrow = null;

    private void Update()
    {
        var startPoint = originPosition;
        var midPoint = originPosition;
        var midPointTwo = originPosition;
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
            midPoint = startPoint + direction * Vector3.Distance(startPoint, playerTransform.position);
            midPointTwo = startPoint + (direction * 1.01f) * Vector3.Distance(startPoint, playerTransform.position);
            endPoint = physicsRaycastHit.point;
        }

        //Set points to original start point and raycast hit point
        var positions = new List<Vector3>();

        //Theoretically, go start-mid, then mid-end with 2 line renderers

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

        if (beamWidth > 0)
        {
            beamWidth = beamWidth - 1f *Time.deltaTime;
            oSprite.transform.localScale = new Vector3(beamWidth, beamWidth, beamWidth);
            //Store this to make code easier to read
            var midDistance = Vector3.Distance(startPoint, midPoint) / Vector3.Distance(startPoint, endPoint);
            //Gradients are awful. Basically, LineRendererColour is the animated colour and EXPlusUltra is effectively just black. floats after are just % through the total line renderer
            myGradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(lineRendererColour, 0.0f), new GradientColorKey(lineRendererColour, midDistance), new GradientColorKey(lineRendererColourEXPlusUltra, (midDistance * 1.01f)), new GradientColorKey(lineRendererColourEXPlusUltra, 1) },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, midDistance), new GradientAlphaKey(0f, 1f)}
                );
            targetingBeam.colorGradient = myGradient;
        }
        else
        {
            if (inHitterRadius)
            {
                gameObject.GetComponent<fielderTargetingSuccessfulHit>().SpawnTheBaseballPrefabAtThePlayerAndHitItRealHard(midPoint);
            }
            else
            {
                gameObject.GetComponent<fielderTargetingBallSpawner>().SpawnTheBaseballPrefabAndThrowItAtTheTarget();
            }
            Destroy(myArrow);
            Destroy(gameObject);
        }
    }
}
