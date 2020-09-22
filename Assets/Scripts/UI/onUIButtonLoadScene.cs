using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onUIButtonLoadScene : MonoBehaviour
{
    [Header("Make sure this is spelled right")]
    [SerializeField] private string sceneToLoad = "Change This In Editor";

    private bool hasStayed = false;

    public void loadTheScene()
    {
        if (hasStayed)
        {
            Debug.LogError("If you're seeing this then the scene name is spelled wrong on '" + gameObject.name + "'");
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
            hasStayed = true;
        }
    }
}
