using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputAction;
    private bool JumpPress;
    private Vector2 moveInput;

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float playerSpeed;
    private Vector3 playerVelocity;
    public float rotationSpeed;
    private float gravityForce = -9.81f;
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
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        moveInput = playerInputAction.Character.Move.ReadValue<Vector2>();
    }
    void Inputs()
    {
        JumpPress = Input_Manager._INPUT_MANAGER.GetSpaceButtonPressed();
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
        Debug.Log(direction);
        playerVelocity.x = direction.x * playerSpeed;
        playerVelocity.z = direction.z * playerSpeed;
        if (direction != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            gameObject.transform.forward = direction;
        }
    }
}
