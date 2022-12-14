using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Input_Manager : MonoBehaviour
{
    #region Inputs
    private PlayerInputActions playerInputs;
    public static Input_Manager _INPUT_MANAGER;
    #endregion
    #region timeSince
    private float timeSinceJumpPressed = 0f;
    private float timeSinceShiftPressed = 0f;
    private float timeSinceShiftDespressed = 0f;
    private float timeSinceCrouchPressed = 0f;
    private float timeSinceCrouchDespressed = 0f;
    private float timeSincePausePressed = 0f;
    private float timeSinceDashPressed = 0f;
    #endregion
    private Vector2 mousePosition = Vector2.zero;
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
            #region LogicInput
            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.Sprint.started += ShiftButtonPressed;
            playerInputs.Character.Sprint.canceled += ShiftButtonDespressed;
            playerInputs.Character.Crouch.started += CrouchButtonPressed;
            playerInputs.Character.Crouch.canceled += CrouchButtonDespressed;
            playerInputs.Character.Dash.performed += DashButtonPressed;
            playerInputs.Character.View.performed += MousePositionUpdate;
            #endregion
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Update()
    {
        #region Timers
        timeSinceJumpPressed += Time.deltaTime;
        timeSinceShiftPressed += Time.deltaTime;
        timeSinceShiftDespressed += Time.deltaTime;
        timeSinceCrouchPressed += Time.deltaTime;
        timeSinceCrouchDespressed += Time.deltaTime;
        timeSinceDashPressed += Time.deltaTime;
        timeSincePausePressed += Time.deltaTime;
        #endregion
        InputSystem.Update();
    }
    #region Move
    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();
    }
    #endregion
    #region Sprint
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
    #endregion
    #region Jump
    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0;
    }
    public bool GetSpaceButtonPressed()
    {
        return this.timeSinceJumpPressed == 0f;
    }
    #endregion
    #region Crouch
    //Crouch Move start
    private void CrouchButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceCrouchPressed = 0;
    }
    public bool GetCrouchButtonPressed()
    {
        return this.timeSinceCrouchPressed == 0f;
    }
    //Crouch Move canceled
    private void CrouchButtonDespressed(InputAction.CallbackContext context)
    {
        timeSinceCrouchDespressed = 0;
    }
    public bool GetCrouchButtonDespressed()
    {
        return this.timeSinceCrouchDespressed == 0f;
    }
    #endregion
    #region Dash
    private void DashButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceDashPressed = 0;
    }
    public bool GetDashButtonPressed()
    {
        return this.timeSinceDashPressed == 0f;
    }
    #endregion
    #region Mouse
    private void MousePositionUpdate(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
    #endregion
}
