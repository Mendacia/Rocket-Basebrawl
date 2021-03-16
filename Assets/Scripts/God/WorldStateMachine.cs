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
            BattingPhase.enabled = false;
            PeltingScript.enabled = true;
            BattingPhase.StopMe();
            PeltingScript.StartMe();
            BallGod.masterBallList.Clear();
            PeltingScript.InitializeRunningPhase();
            currentState = WorldState.RUNNING;
        }

        if (state == WorldState.BATTING)
        {
            BattingPhase.enabled = true;
            PeltingScript.enabled = false;
            BallGod.masterBallList.Clear();
            BattingPhase.StartMe();
            PeltingScript.StopMe();
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
