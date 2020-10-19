using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempBattingControls : MonoBehaviour
{
    [SerializeField] private KeyCode hitKey = KeyCode.Mouse0;
    [SerializeField] private Text devHittingCheckText = null;
    private bool isHitting = false;
    private BoxCollider myCollider = null;

    private void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider>();
        myCollider.enabled = false;
        devHittingCheckText.text = ("IDLE");
        isHitting = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(hitKey) && isHitting == false)
        {
            myCollider.enabled = true;
            devHittingCheckText.text = ("HITTING");
            isHitting = true;
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        myCollider.enabled = false;
        devHittingCheckText.text = ("IDLE");
        isHitting = false;
    }
}
