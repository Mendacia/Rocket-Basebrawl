using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onUIButtonLoadScene : MonoBehaviour
{
    [SerializeField] private LoadingScreenControls loadingUI;
    [Header("Make sure this is spelled right")]
    [SerializeField] private string sceneToLoad = "Change This In Editor";

    private void Start()
    {
        //loadingUI = GameObject.Find("Loading Screen UI").GetComponent<LoadingScreenControls>();
        
    }

    public void loadTheScene()
    {
        loadingUI.CommenceLoading();
        //SceneManager.LoadScene(sceneToLoad);
        StartCoroutine(ActuallyLoadTheScene());
    }

    IEnumerator ActuallyLoadTheScene()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Loading");
        SceneManager.LoadScene(sceneToLoad);
    }
}
