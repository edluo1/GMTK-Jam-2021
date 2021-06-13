using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireEndpointSetter : MonoBehaviour
{
    [SerializeField] private Transform leftWirePos;
    [SerializeField] private Transform rightWirePos;

    [SerializeField] private Rope rope;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.position.x > transform.position.x) {
            rope.SetRopeEnd(rightWirePos);
        }
        else if (other.gameObject.transform.position.x > transform.position.x) {
            rope.SetRopeEnd(leftWirePos);
        }
    }
}
