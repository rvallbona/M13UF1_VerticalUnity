using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] PlayerGame playerGame;
    public int value = 1;
    GameObject bottle;
    private void Start()
    {
        bottle = this.gameObject;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGame.Coin(value);
            Destroy(bottle);
        }
    }
}
