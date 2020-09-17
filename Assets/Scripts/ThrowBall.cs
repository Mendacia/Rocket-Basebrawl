using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ball;
    public Transform ballSpawn, player;
    public float throwRate;
    bool isThrowing = false;

    void Update()
    {
        if(isThrowing == false)
        {
            StartCoroutine(BallThrow());
        }
        var lookDir = player.position - transform.position;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);
        var playerT = GameObject.FindGameObjectWithTag("Player");
        player = playerT.transform;
    }

    IEnumerator BallThrow()
    {
        isThrowing = true;
        Instantiate(ball, ballSpawn.position, transform.rotation);
        if (TimeSlow.timeSlowed == true)
        {
            yield return new WaitForSeconds(throwRate / 2);
        }
        else
        {
            yield return new WaitForSeconds(throwRate);
        }
        isThrowing = false;
    }
}
