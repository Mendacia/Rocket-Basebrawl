using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingLineRenderer : MonoBehaviour
{
    private LineRenderer targetingBeam = null;
    [Header("This, in seconds, is how close to the ball firing you have to hit the ball to get gold")]
    [SerializeField] private float goldThreshold = 0.2f;

    [Header("These are the visual controls for the linerenderer")]
    [SerializeField] private Color lineRendererColorSilver;
    [SerializeField] private Color lineRendererColorGold;
    [SerializeField] private Color lineRendererColorBehindPlayer;
    [SerializeField] private LayerMask hitterLayerMask;
    [SerializeField] private LayerMask physicsLayerMask;
    [SerializeField] private GameObject oSprite = null;
    [SerializeField] private GameObject xSprite = null;
    [SerializeField] private Gradient myGradient = new Gradient();
    [SerializeField] private GameObject arrowRoot;

    [Header("BallHitEffect")]
    [SerializeField] private GameObject ballEffect = null;

    private float currentBeamTime = 0;

    private float beamWidth = 1f;
    private Vector3 direction = Vector3.zero;
    private Vector3 originPosition = Vector3.zero;
    private Transform playerTransform = null;
    private bool sweetSpotActive = false;

    private float recievedBeamLifetime;
    private int recievedIndex;
    private scoreUpdater myScoreUpdater;
    private soundEffectHolder soundFX;
    private BallList myBallList;
    private Vector3 lastFrameMidPoint = Vector3.zero;
    private UIArrow myArrow;

    private Animator fielderAnimator;

    private void Awake()
    {
        targetingBeam = gameObject.GetComponent<LineRenderer>();
        myScoreUpdater = GameObject.Find("ScoreUpdater").GetComponent<scoreUpdater>();
        myBallList = GameObject.Find("BallGod").GetComponent<BallList>();
        soundFX = GameObject.Find("SoundEffectHolder").GetComponent<soundEffectHolder>();
    }

    public void SetUp(float lifetime, int index, Transform player, Transform fielder, Vector3 recievedDirection, Transform arrowFolder)
    {
        recievedBeamLifetime = lifetime;
        recievedIndex = index;
        playerTransform = player;
        originPosition = fielder.position;
        direction = recievedDirection;
        fielderAnimator = fielder.GetComponentInChildren<Animator>();
        myArrow = Instantiate(arrowRoot, arrowFolder).GetComponent<UIArrow>();
    }

    private void Update()
    {
        //Setup for Linerenderer positions
        var startPoint = originPosition;
        var midPoint = originPosition;
        var midPointTwo = originPosition;
        var endPoint = originPosition;

        //This is where the player actually hits the ball
        if (Physics.Raycast(originPosition, direction, out var hitterRayCastHit, 1000, hitterLayerMask, QueryTriggerInteraction.Collide))
        {
            midPoint = hitterRayCastHit.point;
            if (lastFrameMidPoint == Vector3.zero) { lastFrameMidPoint = hitterRayCastHit.point; }
            fire(true, midPoint);
        }
        //This is where the player has not hit the ball
        if (Physics.Raycast(originPosition, direction, out var physicsRaycastHit, 1000, physicsLayerMask))
        {
            midPoint = startPoint + direction * Vector3.Distance(startPoint, playerTransform.position);
            midPointTwo = startPoint + (direction * 1.01f) * Vector3.Distance(startPoint, playerTransform.position);
            endPoint = physicsRaycastHit.point;
        }

        //Setup for the actual raycast's positions
        var positions = new List<Vector3>();

        //Inputting the raycast's positions
        positions.Add(startPoint);
        positions.Add(midPoint);
        positions.Add(midPointTwo);
        positions.Add(endPoint);

        targetingBeam.positionCount = positions.Count;
        targetingBeam.SetPositions(positions.ToArray());

        //Visuals to help with telling the player where to run to
        oSprite.transform.position = midPoint;
        xSprite.transform.position = midPoint;

        //Start shrinking the beam, failing to hit the ball also runs through ShrinkBeam
        targetingBeam.startWidth = beamWidth;
        targetingBeam.endWidth = beamWidth;
        BeamEffectsOverLifetime(midPoint, startPoint, endPoint);

        //Color the beam
        lastFrameMidPoint = midPoint;
    }

    public void GildMe() //Cheat funciton
    {
        sweetSpotActive = true;
    }

    private void BeamEffectsOverLifetime(Vector3 fireAt, Vector3 startPoint, Vector3 endPoint)
    {
        myArrow.giveTheUIArrowTheMidPoint(fireAt);
        if (currentBeamTime < recievedBeamLifetime)
        {
            currentBeamTime = currentBeamTime + Time.deltaTime;
            beamWidth = 1 - (currentBeamTime / recievedBeamLifetime);
            oSprite.transform.localScale = new Vector3(beamWidth, beamWidth, beamWidth);

            //Setting up for gold hits
            if(currentBeamTime > recievedBeamLifetime - goldThreshold)
            {
                sweetSpotActive = true;
                ColorBeamGold(startPoint, fireAt, endPoint);
            }
            else
            {
                ColorBeamSilver(startPoint, fireAt, endPoint);
            }
        }
        else
        {
            fire(false, fireAt);
        }
    }

    private void ColorBeamSilver(Vector3 recievedStartPoint, Vector3 recievedMidPoint, Vector3 recievedEndPoint)
    {
        var midDistance = Vector3.Distance(recievedStartPoint, recievedMidPoint) / Vector3.Distance(recievedStartPoint, recievedEndPoint);
        myGradient.SetKeys(
             /*Colour Keys*/new GradientColorKey[] { new GradientColorKey(lineRendererColorSilver, 0.0f), new GradientColorKey(lineRendererColorSilver, midDistance), new GradientColorKey(lineRendererColorBehindPlayer, (midDistance * 1.01f)), new GradientColorKey(lineRendererColorBehindPlayer, 1) },
             /*Alpha Keys*/new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, midDistance), new GradientAlphaKey(0f, 1f) }
            );
        targetingBeam.colorGradient = myGradient;
        myArrow.myInnerArrow.color = lineRendererColorSilver;
    }
    private void ColorBeamGold(Vector3 recievedStartPoint, Vector3 recievedMidPoint, Vector3 recievedEndPoint)
    {
        var midDistance = Vector3.Distance(recievedStartPoint, recievedMidPoint) / Vector3.Distance(recievedStartPoint, recievedEndPoint);
        myGradient.SetKeys(
             /*Colour Keys*/new GradientColorKey[] { new GradientColorKey(lineRendererColorGold, 0.0f), new GradientColorKey(lineRendererColorGold, midDistance), new GradientColorKey(lineRendererColorBehindPlayer, (midDistance * 1.01f)), new GradientColorKey(lineRendererColorBehindPlayer, 1) },
             /*Alpha Keys*/new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, midDistance), new GradientAlphaKey(0f, 1f) }
            );
        targetingBeam.colorGradient = myGradient;
        myArrow.myInnerArrow.color = lineRendererColorGold;
    }

    private void fire(bool playerHitTheBall, Vector3 midPoint)
    {
        if (playerHitTheBall)
        {
            if (sweetSpotActive)
            {
                myBallList.SetToGold(recievedIndex);
                gameObject.GetComponent<fielderTargetingSuccessfulHit>().SpawnTheBaseballPrefabAndSendItInTheDirectionThePlayerIsFacing(sweetSpotActive, lastFrameMidPoint);
                fielderAnimator.SetTrigger("heFire");
                soundFX.GoldSoundEffect();
                myScoreUpdater.SweetAddToScore(lastFrameMidPoint);
            }
            else
            {
                myBallList.SetToSilver(recievedIndex);
                gameObject.GetComponent<fielderTargetingSuccessfulHit>().SpawnTheBaseballPrefabAndSendItInTheDirectionThePlayerIsFacing(sweetSpotActive, lastFrameMidPoint);
                soundFX.SilverSoundEffect();
                fielderAnimator.SetTrigger("heFire");
                myScoreUpdater.HitAddToScore(lastFrameMidPoint);
            }
        }
        else
        {
            myBallList.SetToMiss(recievedIndex);
            gameObject.GetComponent<fielderTargetingBallSpawner>().SpawnTheBaseballPrefabAndThrowItAtTheTarget(originPosition, direction);
            fielderAnimator.SetTrigger("heFire");
            myScoreUpdater.SubtractFromScore();
        }
        Destroy(myArrow.gameObject);
        Destroy(gameObject);
    }
}
