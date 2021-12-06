using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    public Image filter;
    public Text text;
    private Vector2 pvel;
    public AudioSource pauseEffect;
    public AudioSource mainMusic;

    // Start is called before the first frame update
    void Start()
    {
        mainMusic = this.GetComponent<AudioSource>();
        pauseEffect = GameObject.Find(name: "Pause").GetComponent<AudioSource>();
        filter = GameObject.Find(name: "PauseFilter").GetComponent<Image>();
        text = GameObject.Find(name: "PauseText").GetComponent<Text>();
        filter.enabled = text.enabled = paused = false;
        pvel = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                mainMusic.Pause();
                pauseEffect.Play();
                pvel = GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<Rigidbody2D>().velocity;
                filter.enabled = text.enabled = paused = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseEffect.Play();
                mainMusic.UnPause();
                filter.enabled = text.enabled = paused = false;
                GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<Rigidbody2D>().velocity = pvel;
            }
        }
    }
}