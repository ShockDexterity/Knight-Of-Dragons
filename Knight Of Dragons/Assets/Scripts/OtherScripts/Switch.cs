using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{
    public AudioSource audioSource;
    public SignText signText;
    public Player player;
    public PlayerFrostBreath pfb;
    public Sprite off, on;
    public bool toggleable;
    public bool toggled;
    private string scene;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<Player>();
        pfb = GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<PlayerFrostBreath>();
        toggleable = false;
        toggled = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = off;
        scene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleable && !toggled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (player.totalLoot < 20)
                {
                    signText.ShowText(7);
                }
                else if (scene == "Level_2" && !pfb.granted)
                {
                    signText.ShowText(8);
                }
                else
                {
                    audioSource.Play();
                    this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = on;
                    toggled = true;
                    GameObject.FindGameObjectWithTag("Gate").GetComponent<GateControl>().Open();
                    this.GetComponent<Interactable>().used = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                toggleable = true;
                break;

            // The player didn't walk into it so do nothing
            default: break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        toggleable = false;
    }
}