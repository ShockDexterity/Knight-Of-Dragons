using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBlock : MonoBehaviour
{
    public Animator animator;
    private float shieldLength;
    private float timeBlocked;
    public bool blocking;
    public bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        blocking = false;
        invincible = false;
        shieldLength = (16f / 9f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!invincible && blocking && Time.time >= (timeBlocked + shieldLength))
        {
            invincible = true;
        }
        if (blocking && Time.time >= (timeBlocked + shieldLength + 2.5f))
        {
            blocking = false;
            invincible = false;
            animator.SetBool("Blocking", blocking);
            this.GetComponent<BossDash>().enabled = true;
            this.GetComponent<BossMelee>().enabled = true;
            this.GetComponent<BossController>().enabled = true;
        }
    }

    public void Block()
    {
        GameObject.Find(name: "boss_shield").GetComponent<AudioSource>().Play();
        this.GetComponent<BossDash>().enabled = false;
        this.GetComponent<BossMelee>().enabled = false;
        this.GetComponent<BossController>().enabled = false;
        timeBlocked = Time.time;
        blocking = true;
        animator.SetBool("Blocking", blocking);
        animator.SetTrigger("Blocked");
    }
}