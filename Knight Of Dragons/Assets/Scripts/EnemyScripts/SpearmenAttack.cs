using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmenAttack : MonoBehaviour
{
    public Animator animator;
    public SpearmenController spearmenController;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float attackRange;
    private float timeAttacked;
    private int damage;

    private float hitDelay;
    private bool inAttack;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        spearmenController = this.GetComponent<SpearmenController>();

        hitDelay = 6f / 11f;

        damage = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (inAttack)
        {
            if (Time.time > (timeAttacked + hitDelay))
            {
                inAttack = false;
                Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
                foreach (var player in players)
                {
                    player.GetComponent<Player>().TakeDamage(damage);
                }
            }
        }
    }

    public void Attack()
    {
        timeAttacked = Time.time;
        animator.SetTrigger("Attacking");

        inAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}//end class SpearmenAttack