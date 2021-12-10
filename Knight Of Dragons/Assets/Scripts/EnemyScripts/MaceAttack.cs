using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceAttack : MonoBehaviour
{
    public Animator animator;
    public MaceController maceController;
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
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (maceController == null) { maceController = this.GetComponent<MaceController>(); }

        hitDelay = 9f / 11f;

        damage = 2;
    }//end Start()

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
    }//end Update()

    public void Attack()
    {
        timeAttacked = Time.time;
        animator.SetTrigger("Attacking");
        inAttack = true;
    }//end Attack()

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }//end OnDrawGizmosSelected()
}