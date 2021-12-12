using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAura : MonoBehaviour
{
    private float lastTrigger;
    private int damage;
    public Player player;
    private bool standing;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) { player = GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<Player>(); }
        lastTrigger = -2f;
        damage = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (standing && Time.time >= lastTrigger + 2f)
        {
            lastTrigger = Time.time;
            player.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            standing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            standing = false;
        }
    }
}
