using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovement
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isGrounded;
    private Vector3 velocity;
    private Transform playerTransform;
    private CharacterController playerController;
    private GameInput playerGameInput;

    public PlayerMovement(Transform playerTransform, CharacterController playerController, GameInput playerGameInput)
    {
        this.playerTransform = playerTransform;
        this.playerController = playerController;
        this.playerGameInput = playerGameInput;
    }

    public void Update()
    {
        UpdateMovement(playerTransform, playerController, playerGameInput);
        UpdateRotation(playerTransform, playerGameInput);
    }

    private void UpdateMovement(Transform transform, CharacterController playerController, GameInput playerGameInput)
    {
        CheckGroundInteraction();
        
        Vector2 inputDir = playerGameInput.GetMoveVectorNormalized();
        Vector3 move = (transform.right * inputDir.x + transform.forward * inputDir.y) * this.speed;

        UpdateJump();
        move.y = velocity.y;
        
        playerController.Move(move * Time.deltaTime);
    }

    private void UpdateRotation(Transform transform, GameInput playerGameInput)
    {
        Vector2 rotationInput = playerGameInput.GetLookVectorNormalized();
        rotationInput *= Time.deltaTime;
        transform.Rotate(0, rotationInput.x, 0);
    }

    private void UpdateJump()
    {
        if(playerGameInput.GetJumpingInput())
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = false;
            Debug.unityLogger.Log("Jumping");
        }
        
        velocity.y += gravity * Time.deltaTime;
    }

    private void CheckGroundInteraction()
    {
        if (velocity.y < 0)
        {
            velocity.y = -3;
        }
    }
}
