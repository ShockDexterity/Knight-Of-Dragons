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

    // Start is called before the first frame update
    void Start()
    {
        filter = GameObject.Find("PauseFilter").GetComponent<Image>();
        text = GameObject.Find("PauseText").GetComponent<Text>();
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
                pvel = GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<Rigidbody2D>().velocity;
                filter.enabled = text.enabled = paused = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                filter.enabled = text.enabled = paused = false;
                GameObject.FindGameObjectWithTag(tag: "Player").GetComponent<Rigidbody2D>().velocity = pvel;
            }
        }
    }
}