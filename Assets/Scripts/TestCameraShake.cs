using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraShake : MonoBehaviour
{
    public CinemachineCameraShake camShake;
    public CinemachineCameraShake camShakeAim;

    public void StartShake()
    {
        camShake.Noise(0.75f, 6);
        camShakeAim.Noise(0.75f, 6);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            StartCoroutine(TurnShakeOnAndOff());
        }
    }

    IEnumerator TurnShakeOnAndOff()
    {
        camShake.Noise(0.75f, 6);
        camShakeAim.Noise(0.75f, 6);
        yield return new WaitForSeconds(0.3f);
        camShake.Noise(0, 0);
        camShakeAim.Noise(0, 0);
    }
}
