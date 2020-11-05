using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempOnButtonOpenLoading : MonoBehaviour
{
    [SerializeField] private LoadingScreenControls loadingUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            loadingUI.CommenceLoading();
            StartCoroutine(WaitForLoadingEnd());
        }
    }

    IEnumerator WaitForLoadingEnd()
    {
        yield return new WaitForSeconds(1);
        loadingUI.EndLoading();
    }
}
