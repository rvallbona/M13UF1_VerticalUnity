using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Vector3 lastPosition;
    private HashSet<Transform> onPlatform = new HashSet<Transform>();  // conjuntos para mantener un registro de los jugadores en la plataforma

    private void Awake()
    {
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // calculamos la diferencia de la posición actual y la anterior
        Vector3 deltaPosition = transform.position - lastPosition;

        // movemos todos los objetos en la plataforma
        foreach (Transform trans in onPlatform)
        {
            trans.position += deltaPosition;
        }

        // actualizamos la última posición
        lastPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos si el objeto es el jugador
        if (other.gameObject.tag == "Player")
        {
            // Añadimos al jugador al conjunto onPlatform
            onPlatform.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Comprobamos si el objeto es el jugador
        if (other.gameObject.tag == "Player")
        {
            // Eliminamos al jugador del conjunto onPlatform
            onPlatform.Remove(other.transform);
        }
    }
}
