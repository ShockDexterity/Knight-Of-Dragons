using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D physics;
    private float speed;
    private Vector2 jumpForce;
    public bool jumping;
    private float timeJumped;
    private float jumpDelay;
    private bool inJumpDelay;
    private int dirX;
    private bool canJump;

    private Animator animator;
    private bool idle;
    public bool facingLeft;

    // Start is called before the first frame update
    void Start()
    {
        if (physics == null) { physics = this.GetComponent<Rigidbody2D>(); }
        if (animator == null) { animator = this.GetComponent<Animator>(); }

        jumpDelay = 2f / 5f;

        speed = 3.5f;
        jumpForce = new Vector2(0, 8f);
        jumping = false;
        inJumpDelay = false;
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("jumping: " + jumping);
        // Debug.Log("canJump: " + canJump);
        if (!jumping && (canJump || physics.velocity.y > -0.2f))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                timeJumped = Time.time;
                idle = false;
                jumping = true;
                inJumpDelay = true;
                animator.SetTrigger("Jumped");
                this.GetComponent<PlayerBlock>().enabled = false;
            }
        }
        if (inJumpDelay)
        {
            physics.velocity = Vector2.zero;
            if (Time.time > (timeJumped + jumpDelay))
            {
                physics.AddForce(jumpForce, ForceMode2D.Impulse);
                inJumpDelay = false;
            }
        }
        else
        {
            dirX = (int)Input.GetAxisRaw("Horizontal");

            switch (dirX)
            {
                case 1:
                    facingLeft = false;
                    idle = false;
                    this.transform.localScale = new Vector3(1, 1, 1);
                    break;

                case 0:
                    idle = true;
                    break;

                case -1:
                    facingLeft = true;
                    idle = false;
                    this.transform.localScale = new Vector3(-1, 1, 1);
                    break;
            }
            if (!jumping) { animator.SetBool("Idle", idle); }

            physics.velocity = new Vector2(speed * dirX, physics.velocity.y);
            animator.SetFloat("yVel", physics.velocity.y);
        }
    }//end Update()

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("OnGround", true);
            // No Longer Jumping
            if (jumping)
            {
                jumping = false;
                canJump = true;
                animator.SetTrigger("Landed");
                this.GetComponent<PlayerBlock>().enabled = true;
            }
            else if (!canJump) { canJump = true; }
        }
    }//end OnCollisionEnter2D()

    private void OnCollisionExit2D()
    {
        if (!jumping)
        {
            canJump = false;
        }
        animator.SetBool("OnGround", false);
    }
}//end class PlayerController