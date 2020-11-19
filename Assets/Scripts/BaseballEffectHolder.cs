using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballEffectHolder : MonoBehaviour
{
    [Header("Cinemachine Variables")]
    [SerializeField] private CinemachineCameraShake camShake;
    [SerializeField] private CinemachineCameraShake camShakeAim;
    [SerializeField] private float frequency = 0.8f, amplitude = 3f, waitTime = 0.1f;

    private void Start()
    {
        /*GameObject camShakeMaster = GameObject.FindGameObjectWithTag("CineNormal");
        camShake = camShakeMaster.GetComponent<CinemachineCameraShake>();
        GameObject camShakeAimMaster = GameObject.FindGameObjectWithTag("CineAim");
        camShakeAim = camShakeAimMaster.GetComponent<CinemachineCameraShake>();*/
    }

    public void CameraShakeOnVoid()
    {
        StartCoroutine(TurnShakeOnAndOff());
    }

    public void TimeSlowVoid()
    {
        StartCoroutine(TimeSlowOnHit());
    }

    IEnumerator TurnShakeOnAndOff()
    {
        camShake.Noise(frequency, amplitude);
        camShakeAim.Noise(frequency, amplitude);
        yield return new WaitForSeconds(waitTime);
        camShake.Noise(0, 0);
        camShakeAim.Noise(0, 0);
    }

    IEnumerator TimeSlowOnHit()
    {
        if (PauseMenu.isPaused == false)
        {
            Time.timeScale = 0.3f;
            yield return new WaitForSeconds(0.25f);
            Time.timeScale = 1;
            Debug.Log("This went off");
        }
    }
}
