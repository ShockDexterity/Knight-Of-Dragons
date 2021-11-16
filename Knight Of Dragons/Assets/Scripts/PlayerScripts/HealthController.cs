using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public RectTransform rectTransform;
    //public SpriteRenderer spriteRenderer;
    public Sprite[] healthSprites;

    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        healthSprites = Resources.LoadAll<Sprite>("health");

        Debug.Log("help");

        // healthpng = this.GetComponent<Image>();
        image.sprite = healthSprites[16];

        //spriteRenderer.sprite = healthSprites[16];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealth(int h)
    {
        image.sprite = healthSprites[h];
    }
}