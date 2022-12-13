using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeTutorial : MonoBehaviour
{
    [SerializeField] GameObject mensaje;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");
        mensaje.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Stay");
        mensaje.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exit");
        mensaje.SetActive(false);
    }
}