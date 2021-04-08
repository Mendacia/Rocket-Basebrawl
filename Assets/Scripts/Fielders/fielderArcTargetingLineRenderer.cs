using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderArcTargetingLineRenderer : MonoBehaviour
{
    private LineRenderer targetingBeam = null;
    [Header("This, in seconds, is how close to the ball firing you have to hit the ball to get gold")]
    [SerializeField] private float goldThreshold = 0.2f;

    [Header("These are the visual controls for the linerenderer")]
    [SerializeField] private Color lineRendererColorSilver;
    [SerializeField] private Color lineRendererColorGold;
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

    //This version needs to run it's setup only once, so I need to store some points outside of local
    private Vector3 startPoint, midPoint;

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
        SetUpBeamVisuals();
    }

    private void Update()
    {
        if (Physics.Raycast(new Vector3(midPoint.x, 50, midPoint.z), Vector3.down, out var hitterRayCastHit, 1000, hitterLayerMask, QueryTriggerInteraction.Collide))
        {
            midPoint = hitterRayCastHit.point;
            if (lastFrameMidPoint == Vector3.zero) { lastFrameMidPoint = hitterRayCastHit.point; }
            fire(true);
        }

        //Start shrinking the beam, failing to hit the ball also runs through ShrinkBeam
        targetingBeam.startWidth = beamWidth;
        targetingBeam.endWidth = beamWidth;
        BeamEffectsOverLifetime();

        //Color the beam
        lastFrameMidPoint = midPoint;
    }
    public void SetUpBeamVisuals()
    {
        var tempSP = originPosition;
        var peakPoint = originPosition;
        var tempMP = originPosition;

        //This is where the player has not hit the ball
        if (Physics.Raycast(originPosition, direction, out var physicsRaycastHit, 1000, physicsLayerMask))
        {
            tempMP = tempSP + direction * Vector3.Distance(tempSP, playerTransform.position);
            midPoint = tempMP;
            peakPoint = new Vector3((startPoint.x + midPoint.x) / 2, 50, (startPoint.z + midPoint.z) / 2);
        }

        var positions = new List<Vector3>();

        positions.Add(tempSP);
        positions.Add(peakPoint);
        positions.Add(tempMP);

        targetingBeam.positionCount = positions.Count;
        targetingBeam.SetPositions(positions.ToArray());

        //Visuals to help with telling the player where to run to
        oSprite.transform.position = tempMP;
        xSprite.transform.position = tempMP;
    }//This time I need to set these up and NEVER CHANGE THEM

    public void GildMe() //Cheat funciton
    {
        sweetSpotActive = true;
    }
    private void BeamEffectsOverLifetime()
    {
        myArrow.giveTheUIArrowTheMidPoint(midPoint);
        if (currentBeamTime < recievedBeamLifetime)
        {
            currentBeamTime = currentBeamTime + Time.deltaTime;
            beamWidth = 1 - (currentBeamTime / recievedBeamLifetime);
            oSprite.transform.localScale = new Vector3(beamWidth, beamWidth, beamWidth);

            //Setting up for gold hits
            if (currentBeamTime > recievedBeamLifetime - goldThreshold)
            {
                sweetSpotActive = true;
                ColorBeamGold();
            }
            else
            {
                ColorBeamSilver();
            }
        }
        else
        {
            fire(false);
        }
    }

    private void ColorBeamSilver()
    {
        myGradient.SetKeys(
             /*Colour Keys*/new GradientColorKey[] { new GradientColorKey(lineRendererColorSilver, 0.0f), new GradientColorKey(lineRendererColorSilver, 1.0f) },
             /*Alpha Keys*/new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f)}
            );
        targetingBeam.colorGradient = myGradient;
        myArrow.myInnerArrow.color = lineRendererColorSilver;
    }
    private void ColorBeamGold()
    {
        myGradient.SetKeys(
             /*Colour Keys*/new GradientColorKey[] { new GradientColorKey(lineRendererColorGold, 0.0f), new GradientColorKey(lineRendererColorGold, 1.0f) },
             /*Alpha Keys*/new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
            );
        targetingBeam.colorGradient = myGradient;
        myArrow.myInnerArrow.color = lineRendererColorGold;
    }

    private void fire(bool playerHitTheBall)
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
