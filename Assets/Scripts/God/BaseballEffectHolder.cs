using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Cinemachine;

public class BaseballEffectHolder : MonoBehaviour
{
    [SerializeField] private scoreHolder scoreHold;

    [Header("Cinemachine Variables")]
    [SerializeField] private CinemachineVirtualCamera vcam = null;

    [Header("Post Processing")]
    [SerializeField] private GameObject postProcessingMaster = null;
    [SerializeField] private GameObject postProcessingSub = null;
    [SerializeField] private GameObject vignetteMaster = null;
    [SerializeField] private SphereCollider postProcessingCollider = null;
    [SerializeField] private AudioSource musicSource = null;
    [SerializeField] private Texture dirtTexture = null;
    [SerializeField] private Texture devTexture = null;
    
    private Volume volume = null;
    Bloom bloomLayer = null;
    private Volume vignetteVolume = null;
    Vignette vignetteLayer = null;
    [System.NonSerialized] public float vignetteValue = 0;
    public float ppTime = 0;
    public bool inPPTime = false;

    [Header("Particles")]
    [SerializeField] private GameObject onHitEffect = null;

    //GetComponent is needed to grab the PostProcessing data at start
    private void Start()
    {
        vignetteValue = 0;
        volume = postProcessingMaster.GetComponent<Volume>();
        volume.sharedProfile.TryGet<Bloom>(out bloomLayer);
        bloomLayer.dirtTexture.value = dirtTexture;
        vignetteVolume = vignetteMaster.GetComponent<Volume>();
        vignetteVolume.sharedProfile.TryGet<Vignette>(out vignetteLayer);
    }

    //This is in FixedUpdate to smoothly and consistently change the collider radius size and camera FOV when in post processing time
    private void FixedUpdate()
    {
        //Makes the sphere collider smaller on ball hit to create the effect
        if (inPPTime && postProcessingCollider.radius > 0)
        {
            postProcessingCollider.radius = postProcessingCollider.radius - 0.5f;
        }
        //Zooms out the FOV of the camera on ball hit
        if (inPPTime && vcam.m_Lens.FieldOfView < 55)
        {
            //vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView + 1f;
        }
        //Makes the sphere collider bigger to reset it
        if (!inPPTime && postProcessingCollider.radius < 20)
        {
            postProcessingCollider.radius = postProcessingCollider.radius + 0.8f;
        }
        //Resets the camera back to default FOV
        if (!inPPTime && vcam.m_Lens.FieldOfView > 40)
        {
            vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView - 1.5f;
        }
        //Turns off the local player effect
        if (!inPPTime && postProcessingCollider.radius >= 20)
        {
            postProcessingSub.SetActive(false);
        }
        //Turns off the global effect
        if(!inPPTime && vcam.m_Lens.FieldOfView >= 40)
        {
            postProcessingMaster.SetActive(false);
        }

       /* 
        if(scoreHold.score < 0 && scoreHold.score > -10000)
        {
            //musicSource.pitch = 0.8f;
            if(musicSource.pitch < 0.825)
            {
                musicSource.pitch = musicSource.pitch + 0.025f;
            }
            if (musicSource.pitch > 0.825)
            {
                musicSource.pitch = musicSource.pitch - 0.025f;
            }
        }
        if (scoreHold.score < -10 && scoreHold.score > -20000)
        {
            //musicSource.pitch = 0.5f;
            if (musicSource.pitch < 0.525)
            {
                musicSource.pitch = musicSource.pitch + 0.025f;
            }
            if (musicSource.pitch > 0.525)
            {
                musicSource.pitch = musicSource.pitch - 0.025f;
            }
        }
        if (scoreHold.score < -20 && scoreHold.score > -30000)
        {
            //musicSource.pitch = 0.3f;
            if (musicSource.pitch < 0.325)
            {
                musicSource.pitch = musicSource.pitch + 0.025f;
            }
            if (musicSource.pitch > 0.325)
            {
                musicSource.pitch = musicSource.pitch - 0.025f;
            }
        }
        if (scoreHold.score < -30000)
        {
            //musicSource.pitch = 0.1f;
            if (musicSource.pitch < 0.125)
            {
                musicSource.pitch = musicSource.pitch + 0.025f;
            }
            if (musicSource.pitch > 0.125)
            {
                musicSource.pitch = musicSource.pitch - 0.025f;
            }
        }*/

        //Update Vignette with score
        if (scoreHold.score < 0)
        {
            //vignetteValue = vignetteValue + 0.0005f;
        }
        else if(scoreHold.score >= 1)
        {
            /*if (musicSource.pitch < 1)
            {
                musicSource.pitch = musicSource.pitch + 0.025f;
            }*/
            vignetteValue = vignetteValue - 0.3f;
            if(vignetteValue < 0)
            {
                vignetteValue = 0;
            }
        }

        vignetteLayer.intensity.value = vignetteValue;

        if (vignetteLayer.intensity.value == 1)
        {
            //Enter last stand mode
            //musicSource.pitch = 0.05f;
        }
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

    //Change Dirt Texture Image by setting the value of the Bloom Dirt Texture to a preset Texture in the editor
    public void ChangeDirtTexture()
    {
        if(bloomLayer.dirtTexture.value == dirtTexture)
        {
            bloomLayer.dirtTexture.value = devTexture;
        }
        else
        {
            bloomLayer.dirtTexture.value = dirtTexture;
        }
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
        yield return new WaitForSeconds(0.4f);
        bloomLayer.dirtIntensity.value = 0;
        inPPTime = false;
    }
}