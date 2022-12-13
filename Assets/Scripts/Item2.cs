using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : MonoBehaviour
{
    [SerializeField] PlayerGame playerGame;
    public int value = 1;
    GameObject bottle2;
    private void Start()
    {
        bottle2 = this.gameObject;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGame.Bottle2(value);
            Destroy(bottle2);
        }
    }
}
