using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStateMachine : MonoBehaviour
{
    private static TutorialStateMachine Instance2;
    [SerializeField] private tutorialPeltingScriptBattingPhase BattingPhase = null;
    [SerializeField] private tutorialPeltingScript PeltingScript = null;
    [SerializeField] private tutorialBaseManager BaseScript = null;
    [SerializeField] private BallList BallGod = null;
    [SerializeField] private HUDManager hUDScript = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject pitchingCam = null;
    private TutorialState currentState;
    public static bool isTutorial = false;
    private void Awake()
    {
        if (Instance2 != null)
        {
            Destroy(gameObject);
        }
        Instance2 = this;
        isTutorial = true;
    }

    private void SetCurrentStateInternal(TutorialState state)
    {
        if (state == TutorialState.RUNNING)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = ~RigidbodyConstraints.FreezePositionX & ~RigidbodyConstraints.FreezePositionZ;
            pitchingCam.SetActive(false);
            BattingPhase.StopMe();
            PeltingScript.StartMe();
            BattingPhase.enabled = false;
            PeltingScript.enabled = true;
            hUDScript.clearTheBallUI();
            BallGod.masterBallList.Clear();
            PeltingScript.InitializeRunningPhase();
            currentState = TutorialState.RUNNING;
        }

        if (state == TutorialState.BATTING)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            BattingPhase.StartMe();
            PeltingScript.StopMe();
            pitchingCam.SetActive(true);
            BattingPhase.enabled = true;
            PeltingScript.enabled = false;
            BallGod.masterBallList.Clear();
            hUDScript.clearTheBallUI();
            BattingPhase.InitializeBattingPhase(BaseScript.currentBaseTarget);
            currentState = TutorialState.BATTING;
        }

        if(state == TutorialState.WALKING)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = ~RigidbodyConstraints.FreezeAll;
            pitchingCam.SetActive(false);
            BattingPhase.StopMe();
            PeltingScript.StopMe();
            BattingPhase.enabled = false;
            PeltingScript.enabled = false;
            hUDScript.clearTheBallUI();
            BallGod.masterBallList.Clear();
            currentState = TutorialState.WALKING;
        }

        if (state == TutorialState.FROZEN)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            currentState = TutorialState.FROZEN;
            pitchingCam.SetActive(false);
            BallGod.masterBallList.Clear();
            hUDScript.clearTheBallUI();
            BattingPhase.StopMe();
            PeltingScript.StopMe();
            BattingPhase.enabled = false;
            PeltingScript.enabled = false;
        }
    }

    public static TutorialState GetCurrentState() => Instance2.currentState;

    public static void SetCurrentState(TutorialState state) => Instance2.SetCurrentStateInternal(state);
}

public enum TutorialState
{
    BATTING,
    RUNNING,
    WALKING,
    FROZEN
}
