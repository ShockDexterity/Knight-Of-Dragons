using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    public Animator animator;
    public KnightController knightController;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float attackRange;
    private float attackDelay;
    private float timeAttacked;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (knightController == null) { knightController = this.GetComponent<KnightController>(); }

        attackRange = 0.5f;
        attackDelay = 1.182f;
        damage = 2;
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (!knightController.enabled && Time.time > +(timeAttacked + attackDelay))
        {
            knightController.enabled = true;
        }
    }//end Update()

    public void Attack()
    {
        knightController.enabled = false;
        timeAttacked = Time.time;
        animator.SetTrigger("Attacking");

        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (var player in players)
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }//end Attack()

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }//end OnDrawGizmosSelected()
}//end class KnightAttack