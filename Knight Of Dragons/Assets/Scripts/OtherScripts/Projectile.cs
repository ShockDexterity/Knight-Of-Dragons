using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;

    public Vector3 initialPos;
    private int enemiesHit;

    private char id;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.transform.position;
        enemiesHit = 0;
    }//end Start()

    void FixedUpdate()
    {
        transform.Translate(speed * Time.fixedDeltaTime, 0, 0);
        if (id == 'f' && enemiesHit >= 3)
        {
            Destroy(this.gameObject);
        }
        if (Mathf.Abs(transform.position.x) > 15 + Mathf.Abs(initialPos.x))
        {
            Destroy(this.gameObject);
        }
    }//end FixedUpdate()

    public void Fire(bool left, char cid)
    {
        id = cid;
        speed = (left) ? -3.5f : 3.5f;

        damage = (id == 'f') ? 10 : ((id == 'm') ? 1 : 0);

        if ((left && this.id == 'f') || (!left && this.id == 'm'))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }//end Fire()

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (id == 'f')
        {
            switch (collision.gameObject.tag)
            {
                // Damage the enemy it hits
                case "Enemy":
                    collision.GetComponent<Enemy>().TakeDamage(damage);
                    enemiesHit++;
                    break;

                // Do nothing
                case "Player": break;

                case "ProjPass": break;

                case "NoEnemy": break;

                // It hit some other collider, so it can be destroyed
                default:
                    Destroy(this.gameObject);
                    break;
            }
        }
        else if (id == 'm')
        {
            switch (collision.gameObject.tag)
            {
                // It hit the player and deals damage
                case "Player":
                    collision.GetComponent<Player>().TakeDamage(damage);
                    Destroy(this.gameObject);
                    break;

                // Do nothing
                case "Enemy": break;

                case "NoEnemy": break;

                case "ProjPass": break;

                // It hit some other collider, so it can be destroyed
                default:
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}