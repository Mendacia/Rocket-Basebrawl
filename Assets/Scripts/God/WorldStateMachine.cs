﻿using System.Collections;
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
    [SerializeField] private GameObject player = null;
    [SerializeField] private GameObject pitchingCam = null;
    private WorldState currentState;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void SetCurrentStateInternal(WorldState state)
    {
        if (state == WorldState.RUNNING)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = ~RigidbodyConstraints.FreezeAll;
            pitchingCam.SetActive(false);
            BattingPhase.enabled = false;
            PeltingScript.enabled = true;
            BattingPhase.StopMe();
            PeltingScript.StartMe();
            BallGod.masterBallList.Clear();
            hUDScript.clearTheBallUI();
            PeltingScript.InitializeRunningPhase();
            currentState = WorldState.RUNNING;
        }

        if (state == WorldState.BATTING)
        {
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            pitchingCam.SetActive(true);
            BattingPhase.enabled = true;
            PeltingScript.enabled = false;
            BattingPhase.StartMe();
            PeltingScript.StopMe();
            BallGod.masterBallList.Clear();
            hUDScript.clearTheBallUI();
            BattingPhase.InitializeBattingPhase(BaseScript.currentBaseTarget);
            currentState = WorldState.BATTING;
        }
    }

    public static WorldState GetCurrentState() => Instance.currentState;

    public static void SetCurrentState(WorldState state) => Instance.SetCurrentStateInternal(state);
}

public enum WorldState
{
    BATTING,
    RUNNING,
    FROZEN
}
