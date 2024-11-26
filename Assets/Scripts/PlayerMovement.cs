using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isGrounded;
    
    private CharacterController controller;
    void Start()
    {
        this.controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    
    void Update()
    {

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        Vector2 inputDir = this.gameInput.GetMoveVectorNormalized();
        Vector3 move = transform.right * inputDir.x + transform.forward * inputDir.y;

        controller.Move(move * speed * Time.deltaTime);

        if(this.gameInput.GetJumpingInput() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }

}
