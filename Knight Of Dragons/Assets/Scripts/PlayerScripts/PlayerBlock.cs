using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public Animator animator;
    public bool blocking;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PauseMenu>();
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        blocking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.paused)
        {
            if (!this.GetComponent<PlayerController>().jumping || this.GetComponent<PlayerController>().canJump)
            {
                if (Input.GetMouseButton(1))
                {
                    blocking = true;

                    animator.SetBool("Blocking", blocking);
                    this.GetComponent<PlayerController>().enabled = false;
                    this.GetComponent<PlayerMelee>().enabled = false;
                    this.GetComponent<PlayerFireBreath>().enabled = false;
                    this.GetComponent<PlayerFrostBreath>().enabled = false;
                }
                else if (this.GetComponent<Player>().alive)
                {
                    blocking = false;

                    animator.SetBool("Blocking", blocking);
                    this.GetComponent<PlayerController>().enabled = true;
                    this.GetComponent<PlayerMelee>().enabled = true;
                    this.GetComponent<PlayerFireBreath>().enabled = true;
                    this.GetComponent<PlayerFrostBreath>().enabled = true;
                }
                animator.SetBool("Blocking", blocking);
            }
        }
    }
}
