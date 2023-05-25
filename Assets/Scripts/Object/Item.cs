using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] PlayerGame playerGame;
    public int value = 1;
    AudioSource audioBottle;
    private void Start()
    {
        audioBottle = GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGame.Bottle(value);
            Destroy(gameObject);
            if (audioBottle != null && audioBottle.enabled)
            {
                audioBottle.enabled = true;
                audioBottle.Play();
            }
        }
    }
}
