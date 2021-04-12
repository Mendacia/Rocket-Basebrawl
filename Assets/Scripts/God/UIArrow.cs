using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArrow : MonoBehaviour
{
    private Vector3 target = Vector3.zero;
    public SpriteRenderer myInnerArrow;

    public void giveTheUIArrowTheMidPoint(Vector3 recievedMidPoint)
    {
        target = recievedMidPoint;
    } 
    private void Update()
    {
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
    }
}
