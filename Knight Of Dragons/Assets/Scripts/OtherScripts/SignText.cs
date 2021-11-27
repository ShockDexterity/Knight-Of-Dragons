using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignText : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image image;
    public Sprite[] texts;

    // Start is called before the first frame update
    void Start()
    {
        texts = Resources.LoadAll<Sprite>("signs");
        if (image == null) { image = this.GetComponent<Image>(); }
        image.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowText(int n)
    {
        if (n < texts.Length)
        {
            image.sprite = texts[n];
            image.enabled = true;
        }
    }

    public void HideText()
    {
        image.enabled = false;
    }
}