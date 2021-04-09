using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderScatterAccuracyScript : MonoBehaviour
{
    private Transform playerPosition;
    [SerializeField] private float targetingSphereScaleMaximum = 20f;
    [SerializeField] private float targetingDistanceMaximum = 20f;
    private List<Transform> bases;
    private int nextBaseIndex;
    private int baseAfterIndex;
    private float finalScaleMultiplier;
    private float percentageOfRunRemaining;

    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void NewTargetingNextBaseUpdater(List<Transform> recievedBases, int currentBase)
    {
        bases = recievedBases;
        if (currentBase != recievedBases.Count - 1 && currentBase != recievedBases.Count - 2)
        {
            nextBaseIndex = currentBase + 1;
            baseAfterIndex = currentBase + 2;
        }
        else if (currentBase == recievedBases.Count - 2)
        {
            nextBaseIndex = recievedBases.Count - 1;
            baseAfterIndex = 0;
        }
        else
        {
            nextBaseIndex = 0;
            baseAfterIndex = 1;
        }
    }

    public void updateAccuracysPercentage(float p)
    {
        percentageOfRunRemaining = p;
    }

    void Update()
    {
        var targetingDistanceUpdated = (targetingDistanceMaximum * percentageOfRunRemaining);

        //This is running on an empty so I can see things in inspector, this is just moving it's position
        gameObject.transform.position = playerPosition.position;
        transform.LookAt(bases[nextBaseIndex]);
        gameObject.transform.Translate(0, 0, targetingDistanceUpdated);
        var extraDistance = Vector3.Distance(bases[nextBaseIndex].position, gameObject.transform.position);




        //This is for the case where this object needs to move towards the base after the next      (Which is when the player is too close to next base). 
        if (Vector3.Distance(playerPosition.position, bases[nextBaseIndex].position) < Vector3.Distance(playerPosition.position, gameObject.transform.position))
        {
            gameObject.transform.position = bases[nextBaseIndex].position;
            transform.LookAt(bases[baseAfterIndex]);
            gameObject.transform.Translate(0, 0, extraDistance);
        }

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);




        //Setting the scale of the targeting Sphere:
        finalScaleMultiplier = 1 + (1 - percentageOfRunRemaining);
        var finalScale = finalScaleMultiplier * targetingSphereScaleMaximum;
        gameObject.transform.localScale = new Vector3(finalScale, finalScale, finalScale);
    }

    public Vector3 GiveTheFielderATarget(int iterator, Vector3 previousTarget)
    {
        {
            Vector3 finalTargetPosition;
            if (iterator == 0)
            {
                finalTargetPosition = (gameObject.transform.position + (Random.insideUnitSphere * (finalScaleMultiplier * targetingSphereScaleMaximum) / 2));
                finalTargetPosition.y = 1f;
            }
            else
            {
                var dir = (bases[nextBaseIndex].position - previousTarget).normalized;
                dir = dir * 0.9f;
                finalTargetPosition = previousTarget + dir;
                finalTargetPosition.y = 1;
            }
            return finalTargetPosition;
        }
    }
}
