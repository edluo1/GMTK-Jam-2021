using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField]
    private InputAction action;

    private Animator animator;
    private int cameraSet = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }
    void Start()
    {
        action.performed += _ => SwitchState();
    }

    private void SwitchState()
    {
        if (cameraSet == 0) {
            cameraSet = 1;
        } 
        else if (cameraSet == 1) {
            cameraSet = 0;
        }

        if (cameraSet == 0) {
            animator.Play("Room 1");
        }
        else if (cameraSet == 1) {
            animator.Play("New State 0");
        }
    }
}
