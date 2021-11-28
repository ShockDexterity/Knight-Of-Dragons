using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private float dashLength;
    private float shieldLength;
    private float meleeDelay;

    public Animator animator;

    private float deathLength;

    public const int maxHealth = 47;
    public int health;
    public bool alive;
    private bool underAttack;
    private float timeHurt;

    private float deathTime;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        health = maxHealth;
        alive = true;

        dashLength = 20f / 11f;
        shieldLength = (16f / 9f) + 2.5f;
        meleeDelay = 14f / 11f;

        deathLength = 24f / 9f;
    }

    // Update is called once per frame
    void Update()
    {
        if (underAttack && Time.time >= (timeHurt + 0.3f))
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            underAttack = false;
        }
        if (!alive)
        {
            if (Time.time >= (deathTime + deathLength)) { SceneManager.LoadScene("YouWin"); }
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        if (true)
        {
            health -= incomingDamage;
            if (health < 0) { health = 0; }
            GameObject.Find("BossHealth").GetComponent<BossBar>().UpdateHealth(health);
        }

        if (health < 1)
        {
            deathTime = Time.time;
            alive = false;
            animator.SetBool("Dead", true);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}