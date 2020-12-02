using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimModeSnapping : MonoBehaviour
{
    [SerializeField] private Transform player = null;
    public Vector3 finalTarget = Vector3.zero;
    [SerializeField] private float lerpDistance = 0.8f;
    [SerializeField] private bool timeDriven = true;
    [SerializeField] private float distanceFromTargetPlayerShouldSnapTo = 1;
    private Vector3 fielderPosition;

    private bool hasATargetAlready = false;

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            lerpPlayerToTheAssignedTarget();
        }
    }

    public void assignTargetFromNearestFielderTargetToPlayer(Vector3 recievedTarget, Vector3 recievedPitcher)
    {
        var direction = recievedTarget - player.position;
        direction = new Vector3(direction.x, recievedTarget.y, direction.z);
        finalTarget = recievedTarget + (direction.normalized * -distanceFromTargetPlayerShouldSnapTo);
        fielderPosition = recievedPitcher;
        hasATargetAlready = true;
    }

    public void lerpPlayerToTheAssignedTarget()
    {
        if (hasATargetAlready)
        {
            player.position = Vector3.Lerp(player.position, finalTarget, lerpDistance * (timeDriven ? Time.deltaTime : 1));
            //Need to look at fielderPosition, which, I don't know enough about cinemachine or our implementation of it to do.
        }
    }
}
