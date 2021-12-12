using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject player;
    private float playerX;
    private float bossX;

    public Rigidbody2D physics;
    public Animator animator;
    private bool facingLeft;
    private bool idle;

    private Vector2 vel;
    private bool seesPlayer;
    private int dirX;
    private int choice;
    private float nextChoice;

    public Boss boss;
    public BossBlock bossBlock;
    public BossDash bossDash;
    public BossMelee bossMelee;
    public bool inChoice;

    // Start is called before the first frame update
    void Start()
    {
        inChoice = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (physics == null) { physics = this.GetComponent<Rigidbody2D>(); }
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (boss == null) { boss = this.GetComponent<Boss>(); }
        if (bossBlock == null) { bossBlock = this.GetComponent<BossBlock>(); }
        if (bossDash == null) { bossDash = this.GetComponent<BossDash>(); }
        if (bossMelee == null) { bossMelee = this.GetComponent<BossMelee>(); }

        choice = 1;
        nextChoice = 0f;
        vel = new Vector2(2f, 0f);
        seesPlayer = false;
        this.transform.localScale = new Vector3(-1, 1, 1);
        facingLeft = true;
        idle = true;
        animator.SetBool("Idle", idle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (boss.alive)
        {
            if (!seesPlayer)
            {
                FindPlayer();
            }
            else
            {
                if (this.GetComponent<Boss>().alive && !inChoice)
                {
                    SeekPlayer();
                }
                if (!inChoice && Time.time >= nextChoice)
                {
                    inChoice = true;
                    choice = Random.Range(0, 5);
                    switch (choice)
                    {
                        case 0:
                            bossBlock.Block();
                            nextChoice = Time.time + 1f;
                            break;

                        case 1:
                            bossDash.Dash(left: facingLeft);
                            nextChoice = Time.time + (13f / 11f);
                            break;

                        case 2:
                            bossMelee.MeleeAttack(damage: 4);
                            nextChoice = Time.time + 1f;
                            break;

                        default:
                            nextChoice = Time.time + 1f;
                            break;
                    }
                }
                else
                {
                    inChoice = false;
                }
            }
        }
        else
        {
            this.physics.velocity = Vector2.zero;
        }
    }

    void FindPlayer()
    {
        playerX = player.transform.position.x;
        bossX = this.transform.position.x;

        if (bossX - playerX <= 7f)
        {
            seesPlayer = true;
            return;
        }
    }

    private void SeekPlayer()
    {
        playerX = player.transform.position.x;
        bossX = this.transform.position.x;

        if (Mathf.Abs(playerX - bossX) <= 0.504f)
        {
            dirX = 0;
            if (playerX > bossX)
            {
                this.transform.localScale = Vector3.one;
                this.facingLeft = false;
            }
            else if (playerX < bossX)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                this.facingLeft = true;
            }
        }
        else
        {
            if (playerX > bossX)
            {
                this.transform.localScale = Vector3.one;
                this.facingLeft = false;
                dirX = 1;
            }
            else if (playerX < bossX)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                this.facingLeft = true;
                dirX = -1;
            }
        }
        this.physics.velocity = vel * dirX;

        idle = (Mathf.Abs(physics.velocity.x) < 0.01f) ? true : false;
        animator.SetBool("Idle", idle);
    }//end SeekPlayer()
}