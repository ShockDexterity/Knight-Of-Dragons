using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearmenController : MonoBehaviour
{
    public Enemy enemy;
    public PauseMenu pauseMenu;
    public GameObject player;
    private float playerX;
    private float playerY;
    private float spearmenX;
    private float spearmenY;

    public Rigidbody2D physics;
    public Animator animator;
    private bool facingLeft;
    private bool idle;

    private Vector2 vel;
    private float moveRate;
    private float moveCounter;
    private bool seesPlayer;
    private int dirX;
    public SpearmenAttack spearmenAttack;
    public float attackRate;
    private float nextAttack;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (enemy == null) { enemy = this.GetComponent<Enemy>(); }
        physics = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        spearmenAttack = this.gameObject.GetComponent<SpearmenAttack>();

        vel = new Vector2(1.5f, 0);
        moveRate = 1f;
        moveCounter = 0f;
        seesPlayer = false;
        attackRate = 2f;

        Physics2D.IgnoreLayerCollision(10, 10);
        Physics2D.IgnoreLayerCollision(10, 13);
        Physics2D.IgnoreLayerCollision(13, 13, true);
    }//end Start()

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!pauseMenu.paused || !enemy.alive)
        {
            // While the knight doesn't see the player
            if (!seesPlayer)
            {
                // Update movement
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
                    spearmenAttack.Attack();
                }
            }
        }
    }//end Update()

    private void ChangeDirection()
    {
        // int [-1,2)
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

        spearmenX = this.transform.position.x;
        spearmenY = this.transform.position.y;

        if (Mathf.Abs(playerY - spearmenY) < 2f)
        {
            if (!facingLeft && playerX > spearmenX)
            {
                if (playerX - spearmenX <= 5f) { seesPlayer = true; }
                return;
            }
            else if (facingLeft && playerX < spearmenX)
            {
                if (playerX + 5f > spearmenX) { seesPlayer = true; }
                return;
            }
        }
    }//end FindPlayer()

    private void SeekPlayer()
    {
        playerX = player.transform.position.x;
        spearmenX = this.transform.position.x;

        if (Mathf.Abs(playerX - spearmenX) <= spearmenAttack.attackRange)
        {
            dirX = 0;
            if (playerX > spearmenX)
            {
                this.transform.localScale = Vector3.one;
                this.facingLeft = false;
            }
            else if (playerX < spearmenX)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                this.facingLeft = true;
            }
        }
        else
        {
            if (playerX > spearmenX)
            {
                this.transform.localScale = Vector3.one;
                this.facingLeft = false;
                dirX = 1;
            }
            else if (playerX < spearmenX)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
                this.facingLeft = true;
                dirX = -1;
            }
        }
        this.physics.velocity = new Vector2(vel.x * dirX, this.physics.velocity.y);

        idle = (Mathf.Abs(physics.velocity.x) < 0.01f) ? true : false;
        animator.SetBool("Idle", idle);
    }//end SeekPlayer()
}//end class SpearmenController