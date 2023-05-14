using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] public Transform[] platformPosition;
    private float platformSpeed = 3;
    private int currentPosition = 0;
    private int nextPosition = 1;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        MovePlatform();
    }
    void MovePlatform()
    {
        rb.MovePosition(Vector3.MoveTowards(rb.position, platformPosition[nextPosition].position, platformSpeed * Time.deltaTime));
        if (Vector3.Distance(rb.position, platformPosition[nextPosition].position) <= 0)
        {
            currentPosition = nextPosition;
            nextPosition++;
        }
        if (nextPosition > platformPosition.Length - 1)
        {
            nextPosition = 0;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            if (col.transform.position.y > transform.position.y)
            {
                col.transform.parent = transform;
            }
        }
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            if (col.transform.position.y > transform.position.y)
            {
                col.transform.position = transform.position;
            }
        }
    }

}
