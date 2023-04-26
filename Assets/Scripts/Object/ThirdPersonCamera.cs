using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target; // El objeto que seguirá la cámara
    public float distance = 10.0f; // Distancia de la cámara al objeto
    public float height = 3.0f; // Altura de la cámara respecto al objeto
    public float rotationDamping = 0.5f; // Suavizado de la rotación de la cámara
    public float heightDamping = 0.1f; // Suavizado de la altura de la cámara

    private float currentRotationAngle = 0.0f;
    private float currentHeight = 0.0f;

    void LateUpdate()
    {
        if (!target)
            return;

        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y") * 0.5f;

        currentRotationAngle += mouseX * rotationDamping;
        currentHeight += mouseY * heightDamping;

        currentHeight = Mathf.Clamp(currentHeight, -5, 5);

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        Vector3 position = target.position - currentRotation * Vector3.forward * distance;
        position.y = currentHeight;

        RaycastHit hit;
        Vector3 direction = position - target.position;
        if (Physics.Raycast(target.position, direction, out hit, distance))
        {
            position = hit.point;
        }

        transform.position = position;
        transform.LookAt(target);
    }
}