using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{

    public Animator animator;

    private float deathLength;

    private const float hc = (3f / 2f);
    public const int maxHealth = (int)(47 * hc);
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

        deathLength = 23f / 9f;
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
            if (Time.time >= (deathTime + deathLength)) { SceneManager.LoadScene("Outro"); }
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        if (!this.GetComponent<BossBlock>().invincible)
        {
            health -= incomingDamage;
            if (health < 0) { health = 0; }
            GameObject.Find("BossHealth").GetComponent<BossBar>().UpdateHealth((int)(health / hc));
        }

        if (alive && health < 1)
        {
            GameObject.Find(name: "boss_death").GetComponent<AudioSource>().Play();
            deathTime = Time.time;
            alive = false;
            animator.SetBool("Dead", true);
        }
        else
        {
            timeHurt = Time.time;
            underAttack = true;
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}