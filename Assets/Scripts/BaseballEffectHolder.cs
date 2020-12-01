using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//using UnityEngine.Rendering.PostProcessing;
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
    [SerializeField] private SphereCollider postProcessingCollider = null;
    private Volume volume = null;
    Bloom bloomLayer = null;
    public float ppTime = 0;
    public bool inPPTime = false;

    [Header("Particles")]
    [SerializeField] private GameObject onHitEffect = null;

    //EVERYTHING COMMENTED OUT IS FOR A SYSTEM THAT DEPLETES OVER TIME AND KEEPS
    //THE PLAYER IN POST PROCESSING MODE LONGER FOR EACH BALL THEY HIT

    //This is in update to smoothly and consistently change the collider radius size and camera FOV when in post processing time
    
    private void Start()
    {
        volume = postProcessingMaster.GetComponent<Volume>();
        volume.sharedProfile.TryGet<Bloom>(out bloomLayer);
    }

    private void FixedUpdate()
    {
        if (inPPTime && postProcessingCollider.radius > 0)
        {
            postProcessingCollider.radius = postProcessingCollider.radius - 0.5f;
        }
        if (inPPTime && vcam.m_Lens.FieldOfView < 55)
        {
            vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView + 1f;
        }
        if (!inPPTime && postProcessingCollider.radius < 20)
        {
            postProcessingCollider.radius = postProcessingCollider.radius + 0.8f;
        }
        if (!inPPTime && vcam.m_Lens.FieldOfView > 40)
        {
            vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView - 1.5f;
        }
        if (!inPPTime && postProcessingCollider.radius >= 20)
        {
            postProcessingSub.SetActive(false);
        }
        if(!inPPTime && vcam.m_Lens.FieldOfView >= 40)
        {
            postProcessingMaster.SetActive(false);
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
        bloomLayer.dirtIntensity.value = 1000000;
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
        //postProcessingMaster.SetActive(false);
        //postProcessingSub.SetActive(false);
        //ppCol.radius = 20;
        //vcam.m_Lens.FieldOfView = 40;
        bloomLayer.dirtIntensity.value = 0;
        inPPTime = false;
    }
}
