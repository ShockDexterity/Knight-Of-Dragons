using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool used;

    // Start is called before the first frame update
    void Start()
    {
        used = false;
        if (spriteRenderer == null) { spriteRenderer = this.GetComponent<SpriteRenderer>(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used && collision.gameObject.tag == "Player")
        {
            spriteRenderer.color = new Vector4(0, 0.5f, 1, 1);
        }
    }//end OnTriggerEnter2D()

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spriteRenderer.color = Color.white;
        }
    }//end OnTriggerExit2D()
}