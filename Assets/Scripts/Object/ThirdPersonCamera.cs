using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance;

    private float rotationX = 5.0f;
    private float rotationY = 5.0f;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField] private float cameraLerp;
    [SerializeField] private float mouseSense = 0.5f;

    [SerializeField] Camera cam;
    [SerializeField] GameObject playerController;
    PlayerController player;
    private void Start()
    {
        player = playerController.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!player.isPaused)
        {
            rotationX += Input.GetAxis("Mouse Y") * mouseSense;
            rotationY += Input.GetAxis("Mouse X") * mouseSense;

            rotationX = Mathf.Clamp(rotationX, -30f, 30f);

            transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
            Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * distance, cameraLerp * Time.deltaTime);

            RaycastHit hit;
            if (Physics.Linecast(target.transform.position, finalPosition, out hit))
            {
                finalPosition = hit.point;
                smoothVelocity = Vector3.zero;
            }
            else
            {
                smoothVelocity = Vector3.Lerp(smoothVelocity, (finalPosition - transform.position) * 10f, Time.deltaTime * 20f);
                finalPosition += smoothVelocity * Time.deltaTime;
            }

            transform.position = finalPosition;

            Vector3 direction = Input.GetAxis("Vertical") * cam.transform.forward + Input.GetAxis("Horizontal") * cam.transform.right;
            direction.Normalize();
        }
    }
}