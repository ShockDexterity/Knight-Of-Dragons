using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public GameObject player;
    private int value;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        value = (this.gameObject.name.Substring(0, 4) == "coin") ? 1 : 5;
    }//end Start()

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                player.GetComponent<Player>().AcquireLoot(value);
                Destroy(this.gameObject);
                break;

            case "Walker":
                player.GetComponent<Player>().AcquireLoot(value);
                Destroy(this.gameObject);
                break;

            // The player didn't walk into it so do nothing
            default: break;
        }
    }//end OnTriggerEnter2D()
}//end class Loot