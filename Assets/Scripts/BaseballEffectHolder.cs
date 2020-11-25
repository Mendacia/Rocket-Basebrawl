using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballEffectHolder : MonoBehaviour
{
    [Header("Cinemachine Variables")]
    [SerializeField] private CinemachineCameraShake camShake;
    [SerializeField] private CinemachineCameraShake camShakeAim;
    [SerializeField] private float frequency = 0.8f, amplitude = 3f, waitTime = 0.1f;

    [Header("Post Processing")]
    [SerializeField] private GameObject postProcessingMaster = null;
    [SerializeField] private GameObject postProcessingSub = null;
    [SerializeField] private SphereCollider ppCol = null;
    public float ppTime = 0;
    public bool inPPTime = false;

    //EVERYTHING COMMENTED OUT IS FOR A SYSTEM THAT DEPLETES OVER TIME AND KEEPS
    //THE PLAYER IN POST PROCESSING MODE LONGER FOR EACH BALL THEY HIT


    private void Update()
    {
        if (inPPTime && ppCol.radius > 0)
        {
            ppCol.radius = ppCol.radius - 0.3f;
        }
    }

    public void CameraShakeOnVoid()
    {
        StartCoroutine(TurnShakeOnAndOff());
    }

    public void TimeSlowVoid()
    {
        StartCoroutine(TimeSlowOnHit());
    }

    public void OnHitTurnOnPP()
    {
        StartCoroutine(PostProcessingOnBallHit());
        //StartCoroutine(TickDownThePPTime());
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
        ppCol.radius = 20;
        inPPTime = false;
    }

    IEnumerator TickDownThePPTime()
    {
        if(ppTime > 0)
        {
            ppTime = ppTime - 0.5f;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(TickDownThePPTime());
    }
}
