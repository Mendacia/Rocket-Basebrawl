using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ball;
    public Transform ballSpawn, player;
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
        
    }

    IEnumerator BallThrow()
    {
        isThrowing = true;
        Instantiate(ball, ballSpawn.position, transform.rotation);
        yield return new WaitForSeconds(1f);
        isThrowing = false;
    }
}
