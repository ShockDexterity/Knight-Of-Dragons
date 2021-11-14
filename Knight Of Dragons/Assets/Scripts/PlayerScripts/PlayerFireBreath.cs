using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBreath : MonoBehaviour
{
    public PlayerController playerController;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float attackRate;
    private float nextAttack;
    private float attackRange;
    private int damage;

    private bool attacking;
    private float timeAttackStart;

    private float fireDelay;
    private float duration;

    // Start is called before the first frame update
    void Start()
    {
        if (playerController == null) { playerController = this.GetComponent<PlayerController>(); }
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (spriteRenderer == null) { spriteRenderer = GameObject.Find("fireBreath").GetComponent<SpriteRenderer>(); }
        this.spriteRenderer.enabled = false;

        attackRate = 10f;
        attackRange = 0.5f;
        damage = 16;
        attacking = false;

        fireDelay = 5f / 11f;
        duration = 10f / 11f;
    }//end Start

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("Jumping") && Input.GetKeyDown(KeyCode.F) && (Time.time > nextAttack))
        {
            nextAttack = Time.time + attackRate;
            timeAttackStart = Time.time;
            attacking = true;

            animator.SetTrigger("FireAttack");
        }

        if (attacking && Time.time > (timeAttackStart + fireDelay))
        {
            Attack();
            attacking = false;
            spriteRenderer.enabled = true;
        }
        if (spriteRenderer.enabled && Time.time > (timeAttackStart + duration))
        {
            spriteRenderer.enabled = false;
            playerController.enabled = true;
        }
    }//end Update()

    private void Attack()
    {
        playerController.enabled = false;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (var enemy in enemiesHit)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null) { e.TakeDamage(damage); }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }//end OnDrawGizmosSelected()
}//end class PlayerFireBreath