using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEnabler : MonoBehaviour
{
    [SerializeField] private GameObject leftCamera;
    [SerializeField] private GameObject rightCamera;

    // when the GameObjects collider arrange for this GameObject to travel to the left of the screen
    void OnTriggerEnter2D(Collider2D col)
    {
        leftCamera.SetActive(true);
        rightCamera.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.transform.position.x > transform.position.x) {
            leftCamera.SetActive(false);
        }
        else if (other.gameObject.transform.position.x > transform.position.x) {
            rightCamera.SetActive(false);
        }
    }
}
