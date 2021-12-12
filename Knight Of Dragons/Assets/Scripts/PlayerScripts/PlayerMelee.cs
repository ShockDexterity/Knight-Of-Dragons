using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public AudioSource audioSource;
    public Animator animator;
    public PlayerController playerController;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float attackRate;
    private float nextAttack;
    private float attackRange;
    private float timeAttacked;
    private float attackDelay;
    private float soundDelay;
    private bool playedAudio;
    private int damage;
    private bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>();
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (playerController == null) { playerController = this.GetComponent<PlayerController>(); }

        nextAttack = 0f;
        attackRate = 0.6f;
        attackRange = 0.5f;
        attackDelay = 14f / 20f;
        soundDelay = 6f / 14f;
        playedAudio = true;
        damage = 2;
        canAttack = true;
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused)
        {
            if (canAttack)
            {
                canAttack = false;
                if (!playerController.jumping && Input.GetMouseButtonDown(0) && (Time.time >= nextAttack))
                {
                    animator.SetTrigger("Melee");
                    playedAudio = false;
                    nextAttack = Time.time + attackRate;
                    timeAttacked = Time.time;
                }
            }
            else if (Time.time >= (timeAttacked + attackDelay))
            {
                canAttack = true;
            }
            if (!playedAudio && Time.time > (timeAttacked + soundDelay))
            {
                Attack();
                audioSource.Play();
                playedAudio = true;
            }
        }
    }//end FixedUpdate()

    private void Attack()
    {
        // playerController.enabled = false;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (var enemy in enemiesHit)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            if (e != null) { e.TakeDamage(damage); }
            if (e == null)
            {
                Boss boss = enemy.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.TakeDamage(damage);
                }
            }
        }
    }//end Attack()

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }//end OnDrawGizmosSelected()
}//end class PlayerMelee