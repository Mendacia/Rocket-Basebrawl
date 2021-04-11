using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onUIButtonLoadScene : MonoBehaviour
{
    [SerializeField] private bool useLoadingUI = false;
    [SerializeField] private LoadingScreenControls loadingUI;
    [Header("Make sure this is spelled right")]
    [SerializeField] private string sceneToLoad = "Change This In Editor";

    public void loadTheScene()
    {
        if (useLoadingUI)
        {
            loadingUI.CommenceLoading();
            StartCoroutine(ActuallyLoadTheScene());
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    IEnumerator ActuallyLoadTheScene()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Loading");
        SceneManager.LoadScene(sceneToLoad);
    }
}
