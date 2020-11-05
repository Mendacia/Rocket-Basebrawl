using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onUIButtonLoadScene : MonoBehaviour
{
    private LoadingScreenControls loadingUI;
    [Header("Make sure this is spelled right")]
    [SerializeField] private string sceneToLoad = "Change This In Editor";

    private bool hasStayed = false;

    private void Start()
    {
        loadingUI = GameObject.Find("Loading Screen UI").GetComponent<LoadingScreenControls>();
    }

    public void loadTheScene()
    {
        if (hasStayed)
        {
            Debug.LogError("If you're seeing this then the scene name is spelled wrong on '" + gameObject.name + "'");
        }
        else
        {
            loadingUI.CommenceLoading();
            StartCoroutine(ActuallyLoadTheScene());
        }
    }

    IEnumerator ActuallyLoadTheScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneToLoad);
        hasStayed = true;
    }
}
