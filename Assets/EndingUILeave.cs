using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUILeave : MonoBehaviour
{
    [SerializeField] private GameObject myCanvas, leaderBoardCanvas;

    public void LEAVE()
    {
        myCanvas.GetComponent<Animator>().SetTrigger("Leave");
    }

    IEnumerator DisableTheCanvas()
    {
        yield return new WaitForSeconds(0.34f);
        leaderBoardCanvas.SetActive(true);
        leaderBoardCanvas.GetComponent<Animator>().SetTrigger("Open");
        myCanvas.SetActive(false);
    }
}
