using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fielderTargetingBallSpawner : MonoBehaviour
{
    [Header("Make sure this is actually set up on the prefab")]
    [SerializeField] private GameObject baseballPrefab = null;

    public void SpawnTheBaseballPrefabAndThrowItAtTheTarget(Vector3 oPos, Vector3 dir)
    {
        var myBaseballObject = Instantiate(baseballPrefab);
        myBaseballObject.transform.position = oPos;
        myBaseballObject.transform.LookAt(oPos + dir);
        myBaseballObject.transform.position += new Vector3(0, 0.5f, 1);
    }

    public void SpawnTheBaseballPrefabOnTheFielderAndThrowItUpward(Vector3 oPos, Vector3 ePos)
    {
        var myBaseballObject = Instantiate(baseballPrefab);
        var ballScript = myBaseballObject.GetComponent<fielderPeltingBallBehaviour>();
        ballScript.expireAfterSeconds = 5f;
        myBaseballObject.transform.position = oPos;
        myBaseballObject.transform.LookAt(oPos + Vector3.up);
        myBaseballObject.transform.position += new Vector3(0, 0.5f, 1);
        StartCoroutine(SpawnTheBaseballPrefabAboveTheEndPositionAndLaunchItIntoTheGround(ePos));
        Debug.Log("Completed Spawning");
    }

    IEnumerator SpawnTheBaseballPrefabAboveTheEndPositionAndLaunchItIntoTheGround(Vector3 ePos)
    {
        Debug.Log("Started Coroutine");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Coming Down");
        var myBaseballObject = Instantiate(baseballPrefab);
        var ballScript = myBaseballObject.GetComponent<fielderPeltingBallBehaviour>();
        ballScript.expireAfterSeconds = 0.5f;
        myBaseballObject.transform.position = new Vector3(ePos.x, 50, ePos.z);
        myBaseballObject.transform.LookAt(myBaseballObject.transform.position + Vector3.down);
    }
}
