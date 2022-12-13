using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 mouseInput;
    [SerializeField]float mouseSensitivity = 1.0f;
    [SerializeField] Transform target;
    [SerializeField] float distanceFromTarget = 15f;
    private float rotationX;
    private float rotationY;
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    public float smoothTime = .5f;
    [SerializeField] private float cameraLerp = 12f;
    float mouseX;
    float mouseY;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        mouseInput = new Vector2(mouseX, mouseY);
    }
    void Update()
    {
        rotationX += mouseInput.x * mouseSensitivity;
        rotationY += mouseInput.y * mouseSensitivity;

        rotationY = Mathf.Clamp(rotationY, -70, 90);

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
