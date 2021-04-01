using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingUIPopulateMedals : MonoBehaviour
{
    [Header("Assets/Prefabs/BallUIPostGame")]
    [SerializeField] private GameObject goldBallPrefab;
    [SerializeField] private GameObject silverBallPrefab;
    [SerializeField] private GameObject missedBallPrefab;

    [Header("I could populate this automatically but I can't guarantee that would work")]
    [SerializeField] private Transform goldHolderChild;
    [SerializeField] private Transform silverHolderChild;
    [SerializeField] private Transform missedHolderChild;

    [SerializeField] private float tempCountGold, tempCountSilver, tempCountMissed;
    private float waitTime = 0.1f;
    private int offset = 20;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            PopulateMedalList();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            waitTime = 0.01f;
        }
        else
        {
            waitTime = 0.1f;
        }
    }

    public void PopulateMedalList()
    {

        if (tempCountGold > 0)
        {
            var thisPrefab = Instantiate(goldBallPrefab, goldHolderChild);
            thisPrefab.transform.position += new Vector3(Random.Range(-5, 5), offset, 0);
            offset += 20;
            tempCountGold--;
            if (tempCountGold == 0) { offset = 20; }
            StartCoroutine(waitToContinue());
        }
        else if (tempCountSilver > 0)
        {
            var thisPrefab = Instantiate(silverBallPrefab, silverHolderChild);
            thisPrefab.transform.position += new Vector3(Random.Range(-5, 5), offset, 0);
            offset += 20;
            tempCountSilver--;
            if (tempCountSilver == 0) { offset = 20; }
            StartCoroutine(waitToContinue());
        }
        else if (tempCountMissed > 0)
        {
            var thisPrefab = Instantiate(missedBallPrefab, missedHolderChild);
            thisPrefab.transform.position += new Vector3(Random.Range(-5, 5), offset, 0);
            offset += 20;
            tempCountMissed--;
            if (tempCountMissed == 0) { offset = 20; }
            StartCoroutine(waitToContinue());
        }
    }
    IEnumerator waitToContinue()
    {
        yield return new WaitForSeconds(waitTime);
        PopulateMedalList();
    }
}
