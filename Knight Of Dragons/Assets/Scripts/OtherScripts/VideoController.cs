using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public float clipLength;

    // Start is called before the first frame update
    void Start()
    {
        clipLength = (float)this.gameObject.GetComponent<VideoPlayer>().clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= clipLength)
        {
            if (SceneManager.GetActiveScene().name == "Intro")
            {
                SceneManager.LoadScene("Level_0");
            }
        }
    }
}