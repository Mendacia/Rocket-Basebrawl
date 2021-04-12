using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class BattingControls : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private float hitWindow = 0.5f;
    [SerializeField] private float hitCooldown = 0.5f;
    [SerializeField] private playerDash dashCont = null;
    private bool isHitting = false;
    public bool dashBat = false;
    private BoxCollider myCollider = null;
    [SerializeField] private GameObject battingEffectsUpright = null;
    [SerializeField] private GameObject battingEffectsSliding = null;
    // [SerializeField] private GameObject ventParticles = null;

    [Header("Everything to do with Base Vent Flames")]
    [SerializeField] private Transform ventFlamesRoot = null;
    private List<ParticleSystem> ventFlamesSystems = new List<ParticleSystem>();
    [SerializeField] private float MaximumSize = 1;
    [SerializeField] private float MinimumSize = 0;
    [SerializeField] private float lerpDistance = 0.2f;
    private void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider>();
        myCollider.enabled = false;
        isHitting = false;
    }

    private void Awake()
    {
        foreach (Transform child in ventFlamesRoot)
        {
            ventFlamesSystems.Add(child.GetComponent<ParticleSystem>());
        }
    }
    public void Batting(CallbackContext context)
    {

        switch (dashCont.isDashing)
        {
            case false:
                if (context.performed && isHitting == false && PauseMenu.isPaused == false && WorldStateMachine.GetCurrentState() != WorldState.FROZEN && WorldStateMachine.GetCurrentState() != WorldState.GAMESTART)
                {
                    myCollider.enabled = true;
                    dashBat = true;
                    isHitting = true;
                    playerAnimator.SetTrigger("heHit");
                    battingEffectsUpright.SetActive(true);
                    StartCoroutine(Cooldown());
                }
                break;

            case true:
                if (context.performed && isHitting == false && PauseMenu.isPaused == false && WorldStateMachine.GetCurrentState() != WorldState.FROZEN && WorldStateMachine.GetCurrentState() != WorldState.GAMESTART)
                {
                    myCollider.enabled = true;
                    dashBat = true;
                    isHitting = true;
                    playerAnimator.SetTrigger("heDashHit");
                    battingEffectsSliding.SetActive(true);
                    StartCoroutine(Cooldown());
                }
                break;
        }
    }

    IEnumerator Cooldown()
    {

        foreach(ParticleSystem flame in ventFlamesSystems)
        {
            flame.startSize = MaximumSize;
        }

        yield return new WaitForSeconds(hitWindow);
        myCollider.enabled = false;
        yield return new WaitForSeconds(hitCooldown);
        battingEffectsSliding.SetActive(false);
        battingEffectsUpright.SetActive(false);
        dashBat = false;
        isHitting = false;
    }

    private void Update()
    {
        foreach (ParticleSystem flame in ventFlamesSystems)
        {
            flame.startSize = Mathf.Lerp(flame.startSize, MinimumSize, lerpDistance * Time.deltaTime);
        }
    }
}
