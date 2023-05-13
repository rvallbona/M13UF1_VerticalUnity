using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance = 12.0f;
    [SerializeField] float distanceToIgnore = 10f;

    private float rotationX = 5.0f;
    private float rotationY = 5.0f;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField] private float cameraLerp = 80f;

    [SerializeField] Camera cam;

    private void LateUpdate()
    {
        rotationX += Input.GetAxis("Mouse Y");
        rotationY += Input.GetAxis("Mouse X");

        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position -transform.forward * distance, cameraLerp * Time.deltaTime);
        
        RaycastHit hit;
        if (Physics.Linecast(target.transform.position, finalPosition, out hit))
        {
            //if (hit.distance < distanceToIgnore)
            //{
            //    hit.transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            //}
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