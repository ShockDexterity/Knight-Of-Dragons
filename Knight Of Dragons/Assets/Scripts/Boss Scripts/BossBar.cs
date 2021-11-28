using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossBar : MonoBehaviour
{
    public RectTransform rectTransform;
    public Image image;
    public Sprite[] sprites;
    private int tot;

    // Start is called before the first frame update
    void Start()
    {
        if (image == null) { image = this.GetComponent<Image>(); }
        if (rectTransform == null) { rectTransform = this.GetComponent<RectTransform>(); }
        if (SceneManager.GetActiveScene().name == "Level_Boss")
        {
            sprites = Resources.LoadAll<Sprite>("boss/bossHealthBar");
            tot = sprites.Length - 1;
            image.sprite = sprites[tot];
        }
        else
        {
            Debug.Log("ugh");
            image.enabled = false;
            GameObject.Find("BossBorder").GetComponent<Image>().enabled = false;
        }
    }

    public void UpdateHealth(int h)
    {
        image.sprite = sprites[tot - h];
    }
}
