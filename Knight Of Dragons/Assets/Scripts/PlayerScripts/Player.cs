using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D physics;
    // public HealthController healthController;
    private const int maxHealth = 16;
    public int health;

    public bool alive;
    private float timeOfDeath;

    private int totalLoot;
    private int coinCount;
    private int gemCount;

    private bool hijacked;
    private float timeOfHijack;


    // Start is called before the first frame update
    void Start()
    {
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (physics == null) { physics = this.GetComponent<Rigidbody2D>(); }

        health = maxHealth;
        alive = true;
        hijacked = false;

        totalLoot = coinCount = gemCount = 0;
        // if (healthController == null) { healthController = GameObject.Find("healthIcons").GetComponent<HealthController>(); }
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (hijacked)
        {
            physics.velocity = new Vector2(2.5f, physics.velocity.y);
            if (Time.time >= (timeOfHijack + 3f))
            {
                SceneManager.LoadScene("YouWin");
            }
        }
        if (!alive && Input.GetKeyDown(KeyCode.U) && Time.time >= (timeOfDeath + 5f))
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }//end Update()

    public void AcquireLoot(int value)
    {
        if (value == 1) { coinCount += 1; }
        else { gemCount += 1; }

        totalLoot += value;

        Debug.Log("You acquired some loot! Your total loot is now: " + totalLoot);
    }//end AcquireLoot

    public void Heal(int pot)
    {
        var hpot = health + pot;
        health = ((hpot) > maxHealth) ? maxHealth : hpot;
        // healthController.UpdateHealth(health);
    }//end Heal()

    public void TakeDamage(int incomingDamage)
    {
        if (true)
        {
            health -= incomingDamage;
            // healthController.UpdateHealth((health >= 0) ? health : 0);
        }

        if (health < 1) { this.Die(); }
    }//end TakeDamage()

    private void Die()
    {
        timeOfDeath = Time.time;
        alive = false;

        // this.GetComponent<PlayerBlock>().enabled = false;
        // this.GetComponent<PlayerController>().enabled = false;
        // this.GetComponent<PlayerMelee>().enabled = false;
        // this.GetComponent<PlayerFireBreath>().enabled = false;
        // this.GetComponent<PlayerFrostAttack>().enabled = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) { Destroy(enemy); }

        animator.SetTrigger("Died");
    }

    public void Hijack()
    {
        timeOfHijack = Time.time;
        hijacked = true;

        // this.GetComponent<PlayerBlock>().enabled = false;
        // this.GetComponent<PlayerController>().enabled = false;
        // this.GetComponent<PlayerMelee>().enabled = false;
        // this.GetComponent<PlayerFireBreath>().enabled = false;
        // this.GetComponent<PlayerFrostAttack>().enabled = false;
    }
}//end class Player