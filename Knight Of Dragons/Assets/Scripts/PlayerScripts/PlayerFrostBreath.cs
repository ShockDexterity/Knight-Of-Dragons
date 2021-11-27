using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerFrostBreath : MonoBehaviour
{
    public FrostMeter frostMeter;
    public GameObject frostAttack;
    public Animator animator;
    public Transform attackPoint;
    private float attackRate;
    private float nextAttack;
    private bool attacking;
    private float timeAttackStart;
    public bool madeVisible;
    private float iceDelay;
    public bool granted;

    // Start is called before the first frame update
    void Start()
    {

        if (frostMeter == null) { frostMeter = GameObject.Find("FrostMeter").GetComponent<FrostMeter>(); }
        if (animator == null) { animator = this.GetComponent<Animator>(); }
        attackRate = 10f;
        iceDelay = 5f / 11f;

        var scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "SampleScene":
                granted = true;
                madeVisible = true;
                break;

            case "Level_3":
                granted = true;
                madeVisible = true;
                break;

            default:
                granted = false;
                madeVisible = false;
                break;
        }
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (granted)
        {
            if (!madeVisible)
            {
                madeVisible = true;
                GameObject.Find("FrostImage").GetComponent<Image>().enabled = true;
                GameObject.Find("FrostMeterBorder").GetComponent<Image>().enabled = true;
            }
            if (!animator.GetBool("Jumped") && Input.GetKeyDown(KeyCode.G) && Time.time > nextAttack)
            {
                var t = Time.time;
                nextAttack = t + attackRate;
                timeAttackStart = t;
                attacking = true;
                animator.SetTrigger("FrostAttack");
                frostMeter.Cooldown();
            }

            if (attacking && Time.time > timeAttackStart + iceDelay)
            {
                attacking = false;
                Attack();
            }
        }
    }//end Update()

    private void Attack()
    {
        var facingLeft = this.GetComponent<PlayerController>().facingLeft;
        var proj = Instantiate(frostAttack, attackPoint.position, Quaternion.identity);
        proj.GetComponent<Projectile>().Fire(facingLeft, 'f');
    }//end Attack()
}//end class PlayerFrostBreath