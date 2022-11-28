using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerInputActions playerInputAction;
    private Vector2 mouseInput;

    [SerializeField] float mouseSensitivity;
    [SerializeField] Transform target;
    [SerializeField] float distanceFromTarget;

    private float rotationX;
    private float rotationY;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    public float smoothTime;
    [SerializeField] private float cameraLerp;
    private void Awake()
    {
        playerInputAction = new PlayerInputActions();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        playerInputAction.Character.Enable();
    }
    private void OnDisable()
    {
        playerInputAction.Character.Disable();
    }
    private void FixedUpdate()
    {
        mouseInput = playerInputAction.Character.View.ReadValue<Vector2>();
    }
    void Update()
    {
        rotationX += mouseInput.x * mouseSensitivity;
        rotationY += mouseInput.y * mouseSensitivity;

        rotationY = Mathf.Clamp(rotationY, -60, 90);

        Vector3 nextRotation = new Vector3(rotationY, rotationX);
        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);

        transform.localEulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
    }
    private void LateUpdate()
    {
        Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * distanceFromTarget, cameraLerp * Time.deltaTime);
        RaycastHit hit;
        if (Physics.Linecast(target.transform.position, finalPosition, out hit))
        {
            finalPosition = hit.point;
        }
        transform.position = finalPosition;
    }
}