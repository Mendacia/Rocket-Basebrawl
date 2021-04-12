using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermaUIArrow : MonoBehaviour
{
    [SerializeField] private Transform baseBeacon;
    private Vector3 target = Vector3.zero;
    public SpriteRenderer myInnerArrow;
    private void Update()
    {
        transform.LookAt(new Vector3(baseBeacon.position.x, transform.position.y, baseBeacon.position.z));
    }
}
