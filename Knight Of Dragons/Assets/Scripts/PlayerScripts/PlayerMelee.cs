using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    // public AudioSource audioSource;
    public Animator animator;
    public PlayerController playerController;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float attackRate;
    private float nextAttack;
    private float attackRange;
    private float timeAttacked;
    private float attackDelay;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (playerController == null) { playerController = this.GetComponent<PlayerController>(); }

        attackRate = 0.6f;
        attackRange = 0.5f;
        attackDelay = 0.5f;
        damage = 2;
    }//end Start()

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!animator.GetBool("Jumped") && Input.GetMouseButtonDown(0) && (Time.time >= nextAttack))
        {
            nextAttack = Time.time + attackRate;
            // audioSource.Play();
            Attack();
            timeAttacked = Time.time;
        }
        if (!playerController.enabled && Time.time > (timeAttacked + attackDelay))
        {
            playerController.enabled = true;
        }
    }//end FixedUpdate()

    private void Attack()
    {
        playerController.enabled = false;

        animator.SetTrigger("Melee");

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var enemy in enemiesHit)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null) { e.TakeDamage(damage); }
        }
    }//end Attack()

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }//end OnDrawGizmosSelected()
}//end class PlayerMelee