using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class baseManager : MonoBehaviour
{
    [Header("You shouldn't need to touch these, they're visible for debug purposes")]
    public List<Transform> bases = null;
    public int currentBase = 0;
    public int nextBase = 1;
    [Header("Set these to their respective objects please and thank you")]
    [SerializeField] private Transform playerPosition = null;
    [SerializeField] private Text uIBaseText = null;
    [SerializeField] private fielderTargetingRangeAllocator rangeAllocationScript = null;
    [Header("This is 5 by default")]
    [SerializeField] private float distanceFromBaseRequiredToProgress = 5;
    private bool hasLeftHome = false;
    

    private void Start()
    {
        //Populating list, if bases are wack order the prefab better, if bases break in build, this is probably the issue.
        foreach (Transform child in gameObject.transform)
        {
            bases.Add(child);
        }

        //Sets initial base to home
        currentBase = 0;
    }

    private void Update()
    {
        //Updates the current base if the player is within the distanceFromBaseRequiredToProgress
        if(currentBase != (bases.Count -1))
        {
            if (Vector3.Distance(playerPosition.position, bases[(nextBase = currentBase + 1)].position) < distanceFromBaseRequiredToProgress)
            {
                currentBase = nextBase;
                rangeAllocationScript.RangeAllocatorNextBaseUpdater();
                hasLeftHome = true;
            }
        }
        //This is here because it covers an error where if this was an "or" in the above "if" then the next base would be higher than the index of bases, causing an error.
        else
        {
            if (currentBase == (bases.Count - 1) && Vector3.Distance(playerPosition.position, bases[nextBase = 0].position) < distanceFromBaseRequiredToProgress)
            {
                currentBase = nextBase;
                rangeAllocationScript.RangeAllocatorNextBaseUpdater();
            }
        }

        //Loads scene on return to home, replace this later
        if (currentBase == 0 && hasLeftHome)
        {
            SceneManager.LoadScene("EndingBasebrawlTestingZone");
        }

        uIBaseText.text = bases[currentBase].name;
    }
}
