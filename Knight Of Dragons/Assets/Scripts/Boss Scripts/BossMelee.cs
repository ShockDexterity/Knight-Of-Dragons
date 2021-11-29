using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : MonoBehaviour
{
    public Animator animator;
    public BossController bossController;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float attackRange;
    private float meleeDelay;
    private float timeAttacked;
    // private int damage;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (bossController == null) { bossController = this.GetComponent<BossController>(); }

        attackRange = 0.5f;
        meleeDelay = 14f / 11f;
        // damage = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (!bossController.enabled && Time.time > (timeAttacked + meleeDelay))
        {
            bossController.enabled = true;
        }
    }

    public void MeleeAttack(int damage = 2)
    {
        bossController.enabled = false;
        timeAttacked = Time.time;
        animator.SetTrigger("Melee");

        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (var player in players)
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}