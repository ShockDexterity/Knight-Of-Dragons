using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject coin;
    public GameObject potion;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private float center;
    private int health;
    public bool alive;
    private float deathTime;
    private float deathDelay;
    private bool lootDropped;
    private bool underAttack;
    private float hurtDelay;
    private float timeHurt;
    // private Scene scene;
    private sbyte id;

    // Start is called before the first frame update
    void Start()
    {
        // scene = SceneManager.GetActiveScene();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        center = spriteRenderer.size.y / 2f;

        deathDelay = 8f / 12f;
        hurtDelay = 0.3f;
        alive = true;
        lootDropped = false;

        var name = this.gameObject.name;
        if (name.Contains("archer")) { health = 6; id = 0; }
        else if (name.Contains("mage")) { health = 4; id = 1; }
        else if (name.Contains("mace")) { health = 10; id = 2; }
        else if (name.Contains("spear")) { health = 8; id = 3; }
        else if (name.Contains("knight")) { health = 8; id = 4; }
        else { health = 1; id = -1; }
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (underAttack && Time.time > (timeHurt + hurtDelay))
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            underAttack = false;
        }
        if (!alive)
        {
            if (id == 0) { this.GetComponent<ArcherController>().enabled = false; }
            else if (id == 1) { this.GetComponent<MageController>().enabled = false; }
            else if (id == 2) { this.GetComponent<MaceController>().enabled = false; }
            else if (id == 3) { this.GetComponent<SpearmenController>().enabled = false; }
            else if (id == 4) { this.GetComponent<KnightController>().enabled = false; }

            if (Time.time > (deathTime + deathDelay))
            {
                Destroy(this.gameObject);
            }
        }
    }//end Update()

    public void TakeDamage(int incomingDamage)
    {
        timeHurt = Time.time;
        underAttack = true;

        if (alive) { health -= incomingDamage; }

        if (health < 1)
        {
            alive = false;
            deathTime = timeHurt;
            animator.SetTrigger("Died");
            int r = Random.Range(0, 5);
            if (!lootDropped) { DropLoot(); }
            if (r == 4) { DropPot(); }
        }
        else { this.GetComponent<SpriteRenderer>().color = Color.red; }
    }

    private void DropLoot()
    {
        lootDropped = true;
        CircleCollider2D coinCollider = coin.GetComponent<CircleCollider2D>();
        var coinCenter = coinCollider.radius;

        var xPos = this.transform.position.x;
        var yPos = this.transform.position.y - center + coinCenter;
        var zPos = this.transform.position.z;

        var coinSpawnPoint = new Vector3(xPos, yPos, zPos);

        Instantiate(coin, coinSpawnPoint, Quaternion.identity);
    }

    private void DropPot()
    {
        lootDropped = true;
        BoxCollider2D potionCollider = potion.GetComponent<BoxCollider2D>();
        var potionCenter = potionCollider.size.y / 2;

        var xPos = this.transform.position.x;
        var yPos = this.transform.position.y - center + potionCenter;
        var zPos = this.transform.position.z;

        var potionSpawnPoint = new Vector3(xPos, yPos, zPos);

        Instantiate(potion, potionSpawnPoint, Quaternion.identity);
    }
}//end class Enemy