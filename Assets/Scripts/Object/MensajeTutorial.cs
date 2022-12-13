using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensajeTutorial : MonoBehaviour
{
    [SerializeField] GameObject mensaje, img;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mensaje.SetActive(true);
            img.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            mensaje.SetActive(true);
            img.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            mensaje.SetActive(false);
            img.SetActive(false);
        }
    }
}