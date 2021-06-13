using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControlActions controls;

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Awake() 
    {
        controls = new PlayerControlActions();

        controls.Player.Jump.performed += _ => Jump();
        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<float>());
    }

    // Update is called once per frame
    void Update()
    {
        // horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; 

        // controls.Player.Jump.performed += _ => Jump();
    }

    void Move(float direction)
    {
        horizontalMove = direction * runSpeed;
    }

    void Jump() 
    {
        jump = true;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
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
