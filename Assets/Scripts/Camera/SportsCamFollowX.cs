using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportsCamFollowX : MonoBehaviour
{
    [SerializeField] private GameObject player = null;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }
}
