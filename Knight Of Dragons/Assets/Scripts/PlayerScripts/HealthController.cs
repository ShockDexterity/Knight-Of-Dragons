using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image image;
    public Sprite[] healthSprites;

    // Start is called before the first frame update
    void Start()
    {
        healthSprites = Resources.LoadAll<Sprite>("health");

        image.sprite = healthSprites[16];
    }

    // Update is called once per frame
    void Update()
    {
        /* eop */
    }

    public void UpdateHealth(int h)
    {
        image.sprite = healthSprites[h];
    }
}