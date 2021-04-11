using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BailToEndScreenOnButton : MonoBehaviour
{
    [SerializeField] private KeyCode bailButton;
    [SerializeField] private string sceneToLoad = "LiamBasebrawlEndingZone";
    void Update()
    {
        if (Input.GetKeyDown(bailButton))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
