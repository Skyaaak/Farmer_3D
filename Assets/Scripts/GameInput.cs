using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerControl playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerControl();
    }

    public void OnEnable() {
        playerInputAction.Player.Enable();
    }

    public void OnDisable() {
        playerInputAction.Player.Disable();
    }

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector2 GetMoveVector()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector2 GetMoveVectorNormalized()
    {
        Vector2 moveDir = GetMoveVector().normalized;
        return moveDir;
    }

    public Vector2 GetLookVector()
    {
        return playerInputAction.Player.Look.ReadValue<Vector2>();
    }

    public Vector2 GetLookVectorNormalized() 
    {
        return GetLookVector().normalized;
    }

    public bool GetSprintingInput()
    {
        return playerInputAction.Player.Sprint.IsPressed();
    }

    public bool GetJumpingInput() {
        return playerInputAction.Player.Jump.triggered;
    }

    public bool GetInteractingInput()
    {
        return playerInputAction.Player.Interact.triggered;
    }
}