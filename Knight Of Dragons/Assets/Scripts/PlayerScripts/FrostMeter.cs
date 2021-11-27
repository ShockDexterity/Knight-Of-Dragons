using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrostMeter : MonoBehaviour
{
    public Slider slider;
    public bool inCooldown;
    public float timeUsed;

    // Start is called before the first frame update
    void Start()
    {
        if (slider == null) { slider = this.GetComponent<Slider>(); }
        inCooldown = false;
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level_3":
                GameObject.Find("FrostImage").GetComponent<Image>().enabled = true;
                GameObject.Find("FrostMeterBorder").GetComponent<Image>().enabled = true;
                break;

            default:
                GameObject.Find("FrostImage").GetComponent<Image>().enabled = false;
                GameObject.Find("FrostMeterBorder").GetComponent<Image>().enabled = false;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inCooldown)
        {
            var dif = Time.time - timeUsed;
            slider.value = (dif >= 10f) ? 10f : dif;
            if (dif >= 10f) { inCooldown = false; }
        }
    }

    public void Cooldown()
    {
        timeUsed = Time.time;
        inCooldown = true;
    }
}