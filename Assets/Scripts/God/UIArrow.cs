using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArrow : MonoBehaviour
{
    private Vector3 target;
    private Image myImage;
    [SerializeField] private Image innerArrow;
    public Color myColor;

    private void Awake()
    {
        myImage = gameObject.GetComponentInChildren<Image>();
    }
    public void giveTheUIArrowTheMidPoint(Vector3 recievedMidPoint)
    {
        target = recievedMidPoint;
    } 


    private void Update()
    {
        gameObject.transform.position = transform.parent.position;      //Camera.main.WorldToScreenPoint(target.position);         //Gets the location of pitcher's midpoint but like mapped to the canvas like a reasonable person

        var NormalizedX = Camera.main.WorldToViewportPoint(target).x;
        var NormalizedY = Camera.main.WorldToViewportPoint(target).y;

        if(NormalizedX < 1.0f && NormalizedX > 0.0f && NormalizedY < 1.0f && NormalizedY > 0.0f)
        {
            myImage.gameObject.SetActive(false);
        }
        else
        {
            myImage.gameObject.SetActive(true);
            innerArrow.color = myColor;

            var targetPos = Camera.main.WorldToScreenPoint(target);
            var myPos = transform.position;
            targetPos.x = targetPos.x - myPos.x;
            targetPos.y = targetPos.y - myPos.y;
            var angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            
        }
    }
}
