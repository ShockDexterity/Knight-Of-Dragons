using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceController : MonoBehaviour
{
    public GameObject player;
    private float playerX;
    private float playerY;
    private float maceX;
    private float maceY;

    public Rigidbody2D physics;
    public Animator animator;
    private bool facingLeft;
    private bool idle;

    private Vector2 vel;
    private float moveRate;
    private float moveCounter;
    private bool seesPlayer;
    private int dirX;

    public MaceAttack maceAttack;
    public float attackRate;
    private float nextAttack;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (physics == null) { physics = this.GetComponent<Rigidbody2D>(); }
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (maceAttack == null) { maceAttack = this.GetComponent<MaceAttack>(); }

        vel = new Vector2(1.5f, 0);
        moveRate = 1f;
        moveCounter = 0f;
        seesPlayer = false;
        attackRate = 2f;
    }//end Start()

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!seesPlayer)
        {
            if (moveCounter > moveRate)
            {
                ChangeDirection();
                moveCounter = 0f;
            }

            physics.velocity = new Vector2(vel.x * dirX, physics.velocity.y);

            moveCounter += Time.fixedDeltaTime;

            FindPlayer();
        }
        else
        {
            SeekPlayer();

            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                maceAttack.Attack();
            }
        }
    }//end FixedUpdate()

    private void ChangeDirection()
    {
        dirX = Random.Range(-1, 2);

        switch (dirX)
        {
            case -1:
                idle = false;
                transform.localScale = new Vector3(-1, 1, 1);
                facingLeft = true;
                break;

            case 0:
                idle = true;
                break;

            case 1:
                idle = false;
                transform.localScale = new Vector3(1, 1, 1);
                facingLeft = false;
                break;
        }
        animator.SetBool("Idle", idle);
    }//end ChangeDirection()

    private void FindPlayer()
    {
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;

        maceX = this.transform.position.x;
        maceY = this.transform.position.y;

        if (Mathf.Abs(playerY - maceY) < 2f)
        {
            if (!facingLeft && playerX > maceX)
            {
                if (playerX - maceX <= 5f) { seesPlayer = true; }
                return;
            }
            else if (facingLeft && playerX < maceX)
            {
                if (Mathf.Abs(playerX - maceX) <= 5f) { seesPlayer = true; }
                return;
            }
        }
    }//end FindPlayer()

    private void SeekPlayer()
    {
        playerX = player.transform.position.x;
        maceX = this.transform.position.x;

        if (Mathf.Abs(playerX - maceX) <= maceAttack.attackRange)
        {
            dirX = 0;
            if (playerX > maceX)
            {
                this.transform.localScale = Vector3.one;
                this.facingLeft = false;
            }
            else if (playerX < maceX)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                this.facingLeft = true;
            }
        }
        else
        {
            if (playerX > maceX)
            {
                this.transform.localScale = Vector3.one;
                this.facingLeft = false;
                dirX = 1;
            }
            else if (playerX < maceX)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                this.facingLeft = true;
                dirX = -1;
            }
        }
        this.physics.velocity = vel * dirX;

        if (dirX != 0 && idle) { Debug.Log("uh oh"); }
        idle = (dirX != 0) ? true : false;
        animator.SetBool("Idle", idle);
        Debug.Log("SeekPlayer Mace Idle = " + idle);
    }//end SeekPlayer()
}//end class MaceController