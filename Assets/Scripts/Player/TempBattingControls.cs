using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBattingControls : MonoBehaviour
{
    [SerializeField] private KeyCode hitKey = KeyCode.Mouse0;
    private bool isHitting = false;
    private BoxCollider myCollider = null;

    private void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider>();
        myCollider.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(hitKey) && isHitting == false)
        {
            myCollider.enabled = true;
            isHitting = true;
            StartCoroutine(Cooldown());
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        myCollider.enabled = false;
        isHitting = false;
    }
}
