using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDash : MonoBehaviour
{
    public Animator animator;
    public BossController bossController;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public float attackRange;
    private float timeAttacked;
    private float dashLength;
    private float dashSpeed;
    private int damage;
    public bool inDash;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (bossController == null) { bossController = this.GetComponent<BossController>(); }

        dashSpeed = 3.5f;
        dashLength = 20f / 11f;
        attackRange = 0.5f;
        damage = 2;
        inDash = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inDash)
        {
            this.transform.Translate(new Vector3(dashSpeed * Time.fixedDeltaTime, 0, 0));
        }
        if (!bossController.enabled && Time.time > (timeAttacked + dashLength))
        {
            bossController.enabled = true;
        }
    }

    public void Dash(bool left)
    {
        if (left) { dashSpeed *= -1; }
        this.GetComponent<BossMelee>().MeleeAttack(damage: 4);
    }
}