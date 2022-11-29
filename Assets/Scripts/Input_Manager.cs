using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Input_Manager : MonoBehaviour
{
    private PlayerInputActions playerInputs;
    public static Input_Manager _INPUT_MANAGER;
    private float timeSinceJumpPressed = 0f;
    private float timeSinceShiftPressed = 0f;
    private float timeSinceShiftDespressed = 0f; 
    private Vector2 leftAxisValue = Vector2.zero;
    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            playerInputs = new PlayerInputActions();
            playerInputs.Character.Enable();
            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Sprint.started += ShiftButtonPressed;
            playerInputs.Character.Sprint.canceled += ShiftButtonDespressed;
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;
        timeSinceShiftPressed += Time.deltaTime;
        timeSinceShiftDespressed += Time.deltaTime;
        InputSystem.Update();
    }
    //Move
    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
    }
    //Sprint Move start
    private void ShiftButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceShiftPressed = 0;
    }
    public bool GetShiftButtonPressed()
    {
        return this.timeSinceShiftPressed == 0f;
    }
    //Sprint Move canceled
    private void ShiftButtonDespressed(InputAction.CallbackContext context)
    {
        timeSinceShiftDespressed = 0;
    }
    public bool GetShiftButtonDespressed()
    {
        return this.timeSinceShiftDespressed == 0f;
    }
    //Jump
    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0;
    }
    public bool GetSpaceButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }
}
