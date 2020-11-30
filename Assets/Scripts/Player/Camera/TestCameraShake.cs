using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCameraShake : MonoBehaviour
{
    public CinemachineCameraShake camShake;
    public CinemachineCameraShake camShakeAim;

    [SerializeField] private float frequency = 0.8f, amplitude = 3f, waitTime = 0.1f;
    GameObject player;

    private void Start()
    {
        GameObject camShakeMaster = GameObject.FindGameObjectWithTag("CineNormal");
        camShake = camShakeMaster.GetComponent<CinemachineCameraShake>();
        GameObject camShakeAimMaster = GameObject.FindGameObjectWithTag("CineAim");
        camShakeAim = camShakeAimMaster.GetComponent<CinemachineCameraShake>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider == player.GetComponent<BoxCollider>())
        {
            StartCoroutine(TurnShakeOnAndOff());
        }
        StartCoroutine(TurnShakeOnAndOff());
        //Debug.Log("Collision");
    }

    IEnumerator TurnShakeOnAndOff()
    {
        camShake.Noise(frequency, amplitude);
        camShakeAim.Noise(frequency, amplitude);
        if(Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);
        }
        yield return new WaitForSeconds(waitTime);
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
        camShake.Noise(0, 0);
        camShakeAim.Noise(0, 0);
    }
}
