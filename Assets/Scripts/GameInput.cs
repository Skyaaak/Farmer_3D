using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    [SerializeField] private float mouseSpeed = 100f;
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

    private Vector2 GetMoveVector()
    {
        CheckPlayerInputEnabled();
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector2 GetMoveVectorNormalized()
    {
        Vector2 moveDir = GetMoveVector().normalized;
        return moveDir;
    }

    private Vector2 GetLookVector()
    {
        CheckPlayerInputEnabled();
        return playerInputAction.Player.Look.ReadValue<Vector2>();
    }

    public Vector2 GetLookVectorNormalized() 
    {
        Vector2 lookDir = GetLookVector().normalized;
        lookDir *= this.mouseSpeed;
        return lookDir;
    }

    public bool GetSprintingInput()
    {
        CheckPlayerInputEnabled();
        return playerInputAction.Player.Sprint.IsPressed();
    }

    public bool GetJumpingInput() {
        CheckPlayerInputEnabled();
        return playerInputAction.Player.Jump.triggered;
    }

    public bool GetInteractingInput()
    {
        CheckPlayerInputEnabled();
        return playerInputAction.Player.Interact.triggered;
    }

    public bool GetInventoryInput()
    {
        CheckPlayerInputEnabled();
        return playerInputAction.Player.Inventory.triggered;
    }

    private void CheckPlayerInputEnabled()
    {
        if (playerInputAction.Player.enabled == false)
        {
            throw new Exception("PlayerControl not enabled");
        }
    }
}