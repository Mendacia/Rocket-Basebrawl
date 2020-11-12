using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenControls : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private bool notFirstScene = true;
    private void Awake()
    {
        Time.timeScale = 1;

        anim = gameObject.GetComponent<Animator>();
        if (notFirstScene)
        {
            anim.Play("Loading Out");
        }
    }

    public void CommenceLoading()
    {
        anim.Play("Loading In");
    }

    public void EndLoading()
    {
        anim.Play("Loading Out");
    }
}
