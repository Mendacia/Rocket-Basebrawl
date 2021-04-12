using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHighCostObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> targets;

    public void ToggleObjects(bool on)
    {
        foreach(GameObject target in targets)
        {
            target.SetActive(on);
        }
    }
}
