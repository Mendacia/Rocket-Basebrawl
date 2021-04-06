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
        /*gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        if (target != Vector3.zero)
        {
            innerArrow.color = myColor;

            Vector2 theoreticalIUIElementThatRepresentsTheMidPoint = new Vector2(target.x, target.z);

            var targetPos = new Vector2();
            var myPos = transform.position;
            targetPos.x = myPos.x - theoreticalIUIElementThatRepresentsTheMidPoint.x;
            targetPos.y = myPos.y - theoreticalIUIElementThatRepresentsTheMidPoint.y;
            var angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, angle));
        }*/

        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
    }
}
