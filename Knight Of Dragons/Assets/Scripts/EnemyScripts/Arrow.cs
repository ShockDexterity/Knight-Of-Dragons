using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector3 player;
    private Vector3 initialPosition;
    private int damage;

    public float arcHeight;

    public float speed;

    private float timeFired;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        damage = 1;
    }//end Start()

    // Update is called once per frame
    void FixedUpdate()
    {
        var x0 = initialPosition.x;
        var x1 = player.x;
        var dis = x1 - x0;
        var nextX = Mathf.MoveTowards(this.transform.position.x, x1, speed * Time.fixedDeltaTime);
        var baseY = Mathf.Lerp(initialPosition.y, player.y, (nextX - x0) / dis);
        var arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dis * dis);

        var nextPos = new Vector3(nextX, baseY + arc, this.transform.position.z);

        this.transform.rotation = LookAt2D(nextPos - transform.position);
        transform.position = nextPos;

        if (nextPos == player) { Destroy(this.gameObject); }
    }//end FixedUpdate()

    static Quaternion LookAt2D(Vector3 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    public void Fire()
    {
        timeFired = Time.time;
        initialPosition = this.transform.position;
        if (player.x > initialPosition.x) { transform.localScale = new Vector3(-1, 1, 1); }
    }//end Fire()

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "AttackAllowed": break;
            case "NoEnemy": break;
            case "Enemy": break;

            case "Player":
                var player = collision.gameObject.GetComponent<Player>();
                if (player != null) { player.TakeDamage(damage); }
                Debug.Log("Damage");
                Destroy(this.gameObject);
                break;

            default:
                Destroy(this.gameObject);
                break;
        }
    }//end OnTriggerEnter2D()
}//end class Arrow