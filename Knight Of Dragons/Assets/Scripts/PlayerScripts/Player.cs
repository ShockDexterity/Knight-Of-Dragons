using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public CameraMotor cameraMotor;
    public Transform respawnPoint;
    private bool notMoved;
    public Animator animator;
    public Rigidbody2D physics;
    public HealthController healthController;
    private const int maxHealth = 32;
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
        notMoved = true;
        if (cameraMotor == null) { cameraMotor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMotor>(); }
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        if (physics == null) { physics = this.GetComponent<Rigidbody2D>(); }

        health = maxHealth;
        alive = true;
        hijacked = false;

        totalLoot = coinCount = gemCount = 0;
        if (healthController == null) { healthController = GameObject.Find("Image").GetComponent<HealthController>(); }
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (hijacked)
        {
            physics.velocity = new Vector2(2.5f, physics.velocity.y);
            if (Time.time >= (timeOfHijack + 2f))
            {
                SceneManager.LoadScene("YouWin");
            }
        }
        if (notMoved && !alive && Time.time >= (timeOfDeath + 2f))
        {
            this.transform.position = respawnPoint.position;
            notMoved = false;
            if (!cameraMotor.followPlayer) { cameraMotor.followPlayer = true; }
        }
        if (!alive && Input.GetKeyDown(KeyCode.U) && Time.time >= (timeOfDeath + 2f))
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
        healthController.UpdateHealth(health / 2);
    }//end Heal()

    public void TakeDamage(int incomingDamage)
    {
        if (!this.GetComponent<PlayerBlock>().blocking)
        {
            health -= incomingDamage;
            healthController.UpdateHealth((health > 0) ? health / 2 : 0);
        }

        if (health < 1) { this.Die(); }
    }//end TakeDamage()

    private void Die()
    {
        animator.SetBool("Died", true);
        timeOfDeath = Time.time;
        alive = false;
        Debug.Log(timeOfDeath);
        this.GetComponent<PlayerBlock>().enabled = false;
        this.GetComponent<PlayerController>().enabled = false;
        this.GetComponent<PlayerMelee>().enabled = false;
        this.GetComponent<PlayerFireBreath>().enabled = false;
        this.GetComponent<PlayerFrostBreath>().enabled = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (enemy.tag != "Walker") { Destroy(enemy); }
        }
    }

    public void Hijack()
    {
        timeOfHijack = Time.time;
        hijacked = true;

        this.GetComponent<PlayerBlock>().enabled = false;
        this.GetComponent<PlayerController>().enabled = false;
        this.GetComponent<PlayerMelee>().enabled = false;
        this.GetComponent<PlayerFireBreath>().enabled = false;
        this.GetComponent<PlayerFrostBreath>().enabled = false;
    }
}//end class Player