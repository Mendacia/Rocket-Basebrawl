using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraShake : MonoBehaviour
{
    public CinemachineCameraShake camShake;
    public CinemachineCameraShake camShakeAim;

    [SerializeField] private float frequency = 0, amplitude = 0, waitTime = 0;

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
        camShake.Noise(frequency, amplitude);
        camShakeAim.Noise(frequency, amplitude);
        yield return new WaitForSeconds(waitTime);
        camShake.Noise(0, 0);
        camShakeAim.Noise(0, 0);
    }
}
