using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputAction;
    private bool jumpPress;
    private bool shiftPress = false;
    private Vector2 moveInput;

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    private Vector3 playerVelocity;
    public float rotationSpeed;
    [SerializeField] private float gravityForce;
    private bool groundedPlayer;

    [SerializeField] private GameObject player;
    private CharacterController controller;
    [SerializeField] Camera cam;
    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
    }
    private void OnEnable()
    {
        playerInputAction.Character.Enable();
    }
    private void OnDisable()
    {
        playerInputAction.Character.Disable();
    }
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }
    private void Update()
    {
        Gravity();
        Inputs();
        Movement();
        Jump();
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        moveInput = playerInputAction.Character.Move.ReadValue<Vector2>();
    }
    void Inputs()
    {
        jumpPress = Input_Manager._INPUT_MANAGER.GetSpaceButtonPressed();
        shiftPress = Input_Manager._INPUT_MANAGER.GetShiftButtonPressed();
    }
    void Gravity()
    {
        groundedPlayer = controller.isGrounded;
        if (!groundedPlayer)
        {
            playerVelocity.y += gravityForce * Time.deltaTime;
        }
        else
        {
            playerVelocity.y = gravityForce * Time.deltaTime;
        }
    }
    void Movement()
    {
        
        Vector3 direction = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * new Vector3(moveInput.x, 0, moveInput.y);
        direction.Normalize();
        playerVelocity.x = direction.x * playerSpeed;
        playerVelocity.z = direction.z * playerSpeed;
        if (direction != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            gameObject.transform.forward = direction;
        }
        //if (shiftPress && groundedPlayer)
        //{
        //    playerSpeed = playerSpeed * 2;
        //}
        //else if (!shiftPress)
        //{
        //    playerSpeed = playerSpeed / 2;
        //}
    }
    void Jump()
    {
        if (jumpPress && groundedPlayer)
        {
            playerVelocity.y += jumpForce + gravityForce * Time.deltaTime;
        }
    }
}