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
    [SerializeField] private CinemachineCameraShake camShake = null;
    [SerializeField] private CinemachineCameraShake camShakeAim = null;
    [SerializeField] private float frequency = 0.8f, amplitude = 3f, waitTime = 0.1f;

    [Header("Post Processing")]
    [SerializeField] private GameObject postProcessingMaster = null;
    [SerializeField] private GameObject postProcessingSub = null;
    [SerializeField] private GameObject vignetteMaster = null;
    [SerializeField] private SphereCollider postProcessingCollider = null;
    [SerializeField] private Texture dirtTexture = null;
    [SerializeField] private Texture devTexture = null;
    
    private Volume volume = null;
    Bloom bloomLayer = null;
    private Volume vignetteVolume = null;
    Vignette vignetteLayer = null;
    [System.NonSerialized] public float vignetteValue = 0;
    public float ppTime = 0;
    public bool inPPTime = false;

    [SerializeField] private GameObject ragdoll;
    [SerializeField] private GameObject player;

    [Header("Particles")]
    [SerializeField] private GameObject onHitEffect = null;

    //EVERYTHING COMMENTED OUT IS FOR A SYSTEM THAT DEPLETES OVER TIME AND KEEPS
    //THE PLAYER IN POST PROCESSING MODE LONGER FOR EACH BALL THEY HIT

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

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeDirtTexture();
        }
    }*/

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
            vcam.m_Lens.FieldOfView = vcam.m_Lens.FieldOfView + 1f;
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

        
        //Update Vignette with score
        if(scoreHold.score < 0)
        {
            vignetteValue = vignetteValue + 0.001f;
        }
        else if(scoreHold.score > 1)
        {
            vignetteValue = vignetteValue - 0.3f;
            if(vignetteValue < 0)
            {
                vignetteValue = 0;
            }
        }

        vignetteLayer.intensity.value = vignetteValue;

        if(vignetteLayer.intensity.value == 1)
        {
            StartCoroutine(KillPlayer());
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

    //Ragdoll and kill player
    IEnumerator KillPlayer()
    {
        var myRagdoll = Instantiate(ragdoll, player.transform.position, Quaternion.identity);
        Destroy(player.gameObject);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
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
        bloomLayer.dirtIntensity.value = 0;
        inPPTime = false;
    }
}
