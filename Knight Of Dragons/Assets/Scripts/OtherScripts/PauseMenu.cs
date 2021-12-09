using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    public Image filter;
    public List<Text> texts;
    public List<Image> keys;
    private Vector2 pvel;
    public AudioSource pauseEffect;
    public AudioSource mainMusic;
    private bool hasAudio;

    // Start is called before the first frame update
    void Start()
    {
        hasAudio = (SceneManager.GetActiveScene().name != "Level_0") ? true : false;
        if (hasAudio) { mainMusic = this.GetComponent<AudioSource>(); }
        pauseEffect = GameObject.Find(name: "Pause").GetComponent<AudioSource>();
        filter = GameObject.Find(name: "PauseFilter").GetComponent<Image>();
        texts.Add(GameObject.Find(name: "PauseText").GetComponent<Text>());
        texts.Add(GameObject.Find(name: "PauseControls").GetComponent<Text>());

        keys.Add(GameObject.Find(name: "W").GetComponent<Image>());
        keys.Add(GameObject.Find(name: "A").GetComponent<Image>());
        keys.Add(GameObject.Find(name: "D").GetComponent<Image>());
        keys.Add(GameObject.Find(name: "Space").GetComponent<Image>());
        keys.Add(GameObject.Find(name: "E").GetComponent<Image>());
        keys.Add(GameObject.Find(name: "F").GetComponent<Image>());
        keys.Add(GameObject.Find(name: "G").GetComponent<Image>());
        keys.Add(GameObject.Find(name: "R").GetComponent<Image>());
        keys.Add(GameObject.Find(name: "L").GetComponent<Image>());

        filter.enabled = paused = false;
        foreach (var t in texts) { t.enabled = false; }
        foreach (var k in keys) { k.enabled = false; }
        pvel = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (hasAudio) { mainMusic.Pause(); }
                pauseEffect.Play();
                pvel = GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<Rigidbody2D>().velocity;
                filter.enabled = paused = true;
                foreach (var t in texts) { t.enabled = true; }
                foreach (var k in keys) { k.enabled = true; }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseEffect.Play();
                if (hasAudio) { mainMusic.UnPause(); }
                filter.enabled = paused = false;
                foreach (var t in texts) { t.enabled = false; }
                foreach (var k in keys) { k.enabled = false; }
                GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<Rigidbody2D>().velocity = pvel;
            }
        }
    }
}