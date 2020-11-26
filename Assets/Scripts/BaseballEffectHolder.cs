using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BaseballEffectHolder : MonoBehaviour
{
    [Header("Cinemachine Variables")]
    [SerializeField] private CinemachineVirtualCamera vcam = null;
    [SerializeField] private CinemachineCameraShake camShake = null;
    [SerializeField] private CinemachineCameraShake camShakeAim = null;
    [SerializeField] private float frequency = 0.8f, amplitude = 3f, waitTime = 0.1f;

    [Header("Post Processing")]
    [SerializeField] private GameObject postProcessingMaster = null;
    [SerializeField] private GameObject postProcessingSub = null;
    [SerializeField] private SphereCollider ppCol = null;
    public float ppTime = 0;
    public bool inPPTime = false;

    [Header("Particles")]
    [SerializeField] private GameObject onHitEffect = null;

    //EVERYTHING COMMENTED OUT IS FOR A SYSTEM THAT DEPLETES OVER TIME AND KEEPS
    //THE PLAYER IN POST PROCESSING MODE LONGER FOR EACH BALL THEY HIT

    //This is in update to smoothly and consistently change the collider radius size and camera FOV when in post processing time
    private void Update()
    {
        if (inPPTime && ppCol.radius > 0)
        {
            ppCol.radius = ppCol.radius - 0.3f;
        }
        if (inPPTime && vcam.m_Lens.FieldOfView < 55)
        {
            vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView + 0.75f;
        }
        if (!inPPTime && ppCol.radius < 20)
        {
            ppCol.radius = ppCol.radius + 0.6f;
        }
        if (!inPPTime && vcam.m_Lens.FieldOfView > 40)
        {
            vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView - 1f;
        }
    }
    //Camera Shake
    public void CameraShakeOnVoid()
    {
        StartCoroutine(TurnShakeOnAndOff());
    }
    //Time Slow
    public void TimeSlowVoid()
    {
        StartCoroutine(TimeSlowOnHit());
    }

    //Post Processing
    public void OnHitTurnOnPP()
    {
        StartCoroutine(PostProcessingOnBallHit());
    }

    public void PlayHitEffect(Vector3 ballTransform)
    {
        Instantiate(onHitEffect, ballTransform, Quaternion.identity);
    }

    //Camera shake
    IEnumerator TurnShakeOnAndOff()
    {
        camShake.Noise(frequency, amplitude);
        camShakeAim.Noise(frequency, amplitude);
        yield return new WaitForSeconds(waitTime);
        camShake.Noise(0, 0);
        camShakeAim.Noise(0, 0);
    }


    //Time Slow
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
    //Sets the 2 post processing objects to active and waits for a set time to turn them off
    //Resets the collider radius and post processing bool
    IEnumerator PostProcessingOnBallHit()
    {
        postProcessingMaster.SetActive(true);
        postProcessingSub.SetActive(true);
        /*yield return new WaitForSeconds(1.5f + ppTime);
        Debug.Log(ppTime);
        if(ppTime <= 0)
        {
            postProcessingMaster.SetActive(false);
            inPPTime = false;
        }
        if(inPPTime == true)
        {
            StartCoroutine(PostProcessingOnBallHit());
        }*/
        yield return new WaitForSeconds(0.4f);
        postProcessingMaster.SetActive(false);
        postProcessingSub.SetActive(false);
        //ppCol.radius = 20;
        //vcam.m_Lens.FieldOfView = 40;
        inPPTime = false;
    }
}
