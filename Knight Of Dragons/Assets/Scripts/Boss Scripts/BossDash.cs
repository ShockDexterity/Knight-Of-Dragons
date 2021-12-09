using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDash : MonoBehaviour
{
    public Animator animator;
    public BossController bossController;
    private float timeAttacked;
    private float dashLength;
    private float dashSpeed;
    public bool inDash;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (bossController == null) { bossController = this.GetComponent<BossController>(); }

        dashSpeed = 7f;
        dashLength = 13f / 11f;
        inDash = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inDash)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(dashSpeed, 0);
        }
        if (inDash && Time.time > (timeAttacked + dashLength))
        {
            inDash = false;
            this.GetComponent<BossMelee>().MeleeAttack(damage: 4);
        }
    }

    public void Dash(bool left)
    {
        inDash = true;
        animator.SetTrigger("Dash");
        dashSpeed = (!left) ? 7f : -7f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && inDash)
        {
            GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<Player>().TakeDamage(4);
        }
    }
}