using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTesting : MonoBehaviour
{
    [SerializeField] private string otherObjectTag;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == otherObjectTag)
        {
            Debug.Log("The trigger is doing it's thing, have a good day.");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == otherObjectTag)
        {
            Debug.Log("The collider is doing it's thing, have a good day.");
        }
    }
}
