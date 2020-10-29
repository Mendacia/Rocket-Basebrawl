using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPitcher : MonoBehaviour
{
    [SerializeField] private fielderPeltingScript fielderMain = null;
    private int iterator = 0;

    public IEnumerator BattingPhaseTimer()
    {
        yield return new WaitForSeconds(3);
        iterator++;
        if (iterator >= 4)
        {
            //if they havent hit the ball, then kill them
        }
        else
        {
            fielderMain.battingPhaseThrow();
            BattingPhaseTimer();
        }
    }
}
