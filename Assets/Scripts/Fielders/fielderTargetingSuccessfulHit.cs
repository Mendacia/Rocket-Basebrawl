using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingSuccessfulHit : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;

    public void SpawnTheBaseballPrefabAndSendItInTheDirectionThePlayerIsFacing(bool gold, Vector3 mPos)
    {
        var myBaseballObject = Instantiate(baseballPrefab);
        myBaseballObject.transform.position = mPos;
        myBaseballObject.transform.rotation = Camera.main.transform.rotation;
        myBaseballObject.transform.position = myBaseballObject.transform.position + new Vector3(0, 0.5f, 1);

        if (WorldStateMachine.GetCurrentState() == WorldState.BATTING)
        {
            WorldStateMachine.SetCurrentState(WorldState.RUNNING);
        }
    }

    void PlayHitEffect(Vector3 ballTransform)
    {
        //Instantiate(onHitEffect, ballTransform, Quaternion.identity);
    }
}