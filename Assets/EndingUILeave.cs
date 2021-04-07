using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUILeave : MonoBehaviour
{
    [SerializeField] private GameObject myCanvas, leaderBoardCanvas;

    private void Awake()
    {
        Cursor.visible = true;
    }

    public void LEAVE()
    {
        myCanvas.GetComponent<Animator>().SetTrigger("Leave");
        StartCoroutine(DisableTheCanvas());
    }

    IEnumerator DisableTheCanvas()
    {
        yield return new WaitForSeconds(0.34f);
        leaderBoardCanvas.SetActive(true);
        leaderBoardCanvas.GetComponent<Animator>().SetTrigger("Open");
        myCanvas.SetActive(false);
    }
}
