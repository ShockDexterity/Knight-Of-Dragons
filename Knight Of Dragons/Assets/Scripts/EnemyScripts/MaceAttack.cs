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
    private float attackDelay;
    private float timeAttacked;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (maceController == null) { maceController = this.GetComponent<MaceController>(); }

        attackDelay = 15f / 11f;

        damage = 2;
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (!maceController.enabled && Time.time >= (timeAttacked + attackDelay))
        {
            maceController.enabled = true;
        }
    }//end Update()

    public void Attack()
    {
        maceController.enabled = false;
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
}