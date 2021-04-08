using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateMachine : MonoBehaviour
{
    private static WorldStateMachine Instance;
    [SerializeField] private fielderPeltingScriptBattingPhase BattingPhase = null;
    [SerializeField] private fielderPeltingScript PeltingScript = null;
    [SerializeField] private baseManager BaseScript = null;
    [SerializeField] private BallList BallGod = null;
    [SerializeField] private HUDManager hUDScript = null;
    [SerializeField] private scoreUpdater scoreUpdatingScript = null;
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject pitchingCam = null;
    [SerializeField] private Transform arrowFolder = null;
    private WorldState currentState;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        SetCurrentState(WorldState.GAMESTART);
    }

    private void SetCurrentStateInternal(WorldState state)
    {
        if (state == WorldState.RUNNING)
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
            currentState = WorldState.RUNNING;
        }

        if (state == WorldState.BATTING)
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
            currentState = WorldState.BATTING;
        }
        if (state == WorldState.FROZEN)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            currentState = WorldState.FROZEN;
            //pitchingCam.SetActive(false);
            scoreUpdatingScript.SendNumbersOverToTheScoreHolder();
            BallGod.sendBallsToExporter();
            Debug.Log("Sent balls and values over to DDOL");
            BallGod.masterBallList.Clear();
            hUDScript.clearTheBallUI();
            //Destroy here
            GameObject[] lineRenderers = GameObject.FindGameObjectsWithTag("TargetingBeamTag");
            foreach (GameObject linerender in lineRenderers)
            { 
                Destroy(linerender); 
            }
            foreach (Transform child in arrowFolder)
            {
                Destroy(child.gameObject);
            }
            BattingPhase.StopMe();
            PeltingScript.StopMe();
            BattingPhase.enabled = false;
            PeltingScript.enabled = false;
        }
        if (state == WorldState.GAMESTART)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            currentState = WorldState.GAMESTART;
            pitchingCam.SetActive(false);
            //BallGod.masterBallList.Clear();
            //hUDScript.clearTheBallUI();
            //BattingPhase.StopMe();
            //PeltingScript.StopMe();
            //BattingPhase.enabled = false;
            //PeltingScript.enabled = false;
        }

        if (state == WorldState.FIRSTPITCH)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            //BattingPhase.StartMe();
            pitchingCam.SetActive(true);
            BattingPhase.enabled = true;
            PeltingScript.enabled = false;
            BattingPhase.InitializeBattingPhase(BattingPhase.homeBaseTarget);
            currentState = WorldState.FIRSTPITCH;
            
        }


    }

    public static WorldState GetCurrentState() => Instance.currentState;

    public static void SetCurrentState(WorldState state) => Instance.SetCurrentStateInternal(state);
}

public enum WorldState
{
    BATTING,
    RUNNING,
    GAMESTART,
    FIRSTPITCH,
    FROZEN
}
