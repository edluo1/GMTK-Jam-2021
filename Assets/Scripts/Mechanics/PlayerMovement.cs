using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControlActions controls;

    public CharacterController2D controller;

    public float runSpeed = 40f;

    public bool boostEnabled = false;

    float horizontalMove = 0f;
    bool jump = false;
    bool boost = false;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Awake() 
    {
        controls = new PlayerControlActions();

        controls.Player.Jump.performed += _ => Jump();
        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<float>());
        controls.Player.Boost.performed += _ => Boost(true);
    }

    void Move(float direction)
    {
        horizontalMove = direction * runSpeed;
    }

    void Jump() 
    {
        jump = true;
    }

    void Boost(bool enabled) {
        boost = true;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, boost);
        jump = false;
        boost = false;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
