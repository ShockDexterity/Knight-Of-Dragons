using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    //public float lastTrigger = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //lastTrigger = Time.time;
            collision.GetComponent<Player>().TakeDamage(100);
        }
        else if (collision.tag != "Walker")
        {
            Destroy(collision.gameObject);
        }
    }
}