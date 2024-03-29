using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInputAction;
    private bool jumpPress;
    private bool shiftPress;
    private bool shiftDespress;
    private bool crouchPress;
    private bool crouchDespress;
    private bool dashPress;
    private bool pausePress;
    private Vector2 moveInput;
    Vector3 direction;

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    public bool jumping;
    private bool canDoubleJump;
    private Vector3 playerVelocity;
    public float rotationSpeed;
    const float gravity = -40;
    [SerializeField] private float gravityForce;
    private float gravityScale = 1f;
    private bool groundedPlayer;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashTime;
    [SerializeField] float platformJumpForce;
    [SerializeField] float wallJumpForce;
    private float timerWallJump;

    [SerializeField] private GameObject player;
    private CharacterController controller;

    [SerializeField] Camera cam;
    Animator anim;

    public bool isPaused;
    [SerializeField] GameObject pauseMenuUI, optionsMenuUI, cheatsMenuUI;

    Vector3 groundPosition, lastGroundPosition;
    string groundName, lastGroundName;

    [SerializeField] AudioSource audioInGame, audioMenu; 
    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        gravityForce = gravity;
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
        anim = gameObject.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Inputs();
        wallGravity();
        Gravity();
        Movement();
        Jump();
        PauseMenu();
        //Sprint();
        Crouch();
        WallJump();
        Dash();
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
        shiftDespress = Input_Manager._INPUT_MANAGER.GetShiftButtonDespressed();
        crouchPress = Input_Manager._INPUT_MANAGER.GetCrouchButtonPressed();
        crouchDespress = Input_Manager._INPUT_MANAGER.GetCrouchButtonDespressed();
        dashPress = Input_Manager._INPUT_MANAGER.GetDashButtonPressed();
        pausePress = Input.GetKeyDown(KeyCode.Escape);
    }
    void Gravity()
    {
        groundedPlayer = controller.isGrounded;
        if (!groundedPlayer)
        {
            playerVelocity.y += gravityForce * gravityScale * Time.deltaTime;
        }
        else
        {
            playerVelocity.y = gravityForce * gravityScale * Time.deltaTime;
        }
    }
    void wallGravity()
    {
        if (Physics.Linecast(transform.position, transform.position + transform.forward * 0.7f) && playerVelocity.y <= 0)
        {
            gravityScale = .1f;
        }
        else
        {
            gravityForce = gravity;
            gravityScale = 1;
        }
    }
    void Movement()
    {
        direction = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0) * new Vector3(moveInput.x, 0, moveInput.y);
        direction.Normalize();
        playerVelocity.x = direction.x * playerSpeed;
        playerVelocity.z = direction.z * playerSpeed;
        if (direction != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            gameObject.transform.forward = direction;
        }
        if (groundedPlayer) { anim.SetBool("Jump", false); }
    }
    void Jumping(float jumpForce)
    {
        playerVelocity.y += jumpForce + gravityForce * Time.deltaTime;
    }
    void Jump()
    {
        if (jumpPress && groundedPlayer)
        {
            canDoubleJump = true;
            Jumping(jumpForce);
            anim.SetBool("Jump", true);
        }
        else
        {
            if (jumpPress && canDoubleJump && !groundedPlayer)
            {
                canDoubleJump = false;
                Jumping(jumpForce);
                anim.SetBool("Jump", true);
            }
        }
    }
    private void Sprint()
    {
        if (shiftPress)
        {
            playerSpeed = playerSpeed * 2;
        }
        if (shiftDespress)
        {
            playerSpeed = playerSpeed * .5f;
        }
    }
    private void Crouch()
    {
        if (crouchPress)
        {
            playerSpeed = playerSpeed * .5f;
            anim.SetBool("Crouch", true);
        }
        if (crouchDespress)
        {
            playerSpeed = playerSpeed * 2;
            anim.SetBool("Crouch", false);
        }
    }
    private void Dash()
    {
        if (dashPress)
        {
            Debug.Log("Dash con shift");
            StartCoroutine(Dash());
        }
        IEnumerator Dash()
        {
            float startTime = Time.deltaTime;
            while (Time.time < startTime + dashTime)
            {
                controller.Move(direction * dashSpeed * Time.deltaTime);
                yield return null; 
            }
        }
    }
    public void PlatformJump()
    {
        Jumping(platformJumpForce);
    }
    public float GetCurrentSpeed()
    {
        return this.controller.velocity.magnitude;
    }
    void WallJump()
    {
        timerWallJump += Time.deltaTime;
        if (!groundedPlayer && controller.collisionFlags == CollisionFlags.Sides)
        {
            if (jumpPress && timerWallJump > 1f)
            {
                Jumping(wallJumpForce);
                anim.SetBool("Jump", true);
                timerWallJump = 0;
            }
        }
    }
    void PauseMenu()
    {
        if (pausePress)
        {
            isPaused = !isPaused;
            Pause();
        }
    }
    private void Pause()
    {
        if (isPaused)
        {
            pauseMenuUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            audioMenu.Play();
            audioInGame.Stop();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenuUI.SetActive(false);
            optionsMenuUI.SetActive(false);
            cheatsMenuUI.SetActive(false);
            audioMenu.Stop();
            audioInGame.Play();
            Time.timeScale = 1;
        }
    }
}