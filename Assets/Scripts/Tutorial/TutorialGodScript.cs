using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class TutorialGodScript : MonoBehaviour
{
    [Header("Put Player Controller here!")]
    [SerializeField] private GameObject player = null;
    [SerializeField] private playerControls playerStateReference = null;
    [SerializeField] private CinemachineVirtualCamera playerVCAM = null;
    [Header("Put ScoreHolder here!")]
    [SerializeField] private scoreHolder scoreHold;
    [Header("Put the Opponent Team here!")]
    [SerializeField] private fielderPeltingScript fielderReference = null;

    [Header("UI Elements")]
    //Images
    [SerializeField] private GameObject pitchingPhase = null;
    [SerializeField] private GameObject generalControls = null;
    [SerializeField] private GameObject aimingControls = null;
    [SerializeField] private GameObject battingPhase = null;
    //Buttons
    [SerializeField] private GameObject pitchingButton = null;
    [SerializeField] private GameObject generalButton = null;
    [SerializeField] private GameObject aimingButton = null;
    [SerializeField] private GameObject battingButton = null;
    [SerializeField] private GameObject loadingButton = null;

    private bool isBatting = false;
    public static bool isTutorial = true;

    private void Awake()
    {
        isTutorial = true;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        scoreHold = GameObject.Find("Scoreholder").GetComponent<scoreHolder>();
        Time.timeScale = 0;
        scoreHold.canScore = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    //This is a bunch of BS settings

    private IEnumerator StartPitchingPhase()
    {
        //Resets game variables back to beginning
        fielderReference.hasStartedThrowingSequenceAlready = false;
        playerStateReference.playerState = 1;
        var rb = player.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        player.GetComponent<ActivatePlayer>().enabled = true;
        //Reset camera back to the player, start pitching and increase the base variable
        yield return new WaitForSeconds(2);
        fielderReference.canThrow = false; //Set canThrow here so that it's guaranteed to not conflict with the coroutine before it stops
        StartCoroutine(fielderReference.BattingPhaseTimer());
    }

    //Started from a UI button

    public void StartPitching()
    {
        pitchingPhase.SetActive(false);
        pitchingButton.SetActive(false);
        Time.timeScale = 1;
        playerVCAM.m_Transitions.m_InheritPosition = true;
        Cursor.visible = false;
    }

    public void StartMovement()
    {
        generalControls.SetActive(false);
        generalButton.SetActive(false);
        Time.timeScale = 0;
        aimingControls.SetActive(true);
        aimingButton.SetActive(true);
    }

    public void StartMovement2()
    {
        aimingControls.SetActive(false);
        aimingButton.SetActive(false);
        playerStateReference.playerState = 2;
        playerVCAM.m_Transitions.m_InheritPosition = true;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void ShowBattingUI()
    {
        battingPhase.SetActive(true);
        battingButton.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void StartBatting()
    {
        battingPhase.SetActive(false);
        battingButton.SetActive(false);
        fielderReference.startPeltingLoop();
        playerStateReference.playerState = 2;
        fielderPeltingScript.pitchingLoopStarted = true;
        playerVCAM.m_Transitions.m_InheritPosition = true;
        Time.timeScale = 1;
        isBatting = true;
        Cursor.visible = false;
    }

    public void LoadMainGame()
    {
        isTutorial = false;
        scoreHold.canScore = true;
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if(scoreHold.score >= 3000 && isBatting)
        {
            loadingButton.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
    }
}
