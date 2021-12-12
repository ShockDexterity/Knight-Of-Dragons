using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject prefab;
    public GameObject indicator;
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
        var x = this.transform.position.x;
        var y = this.transform.position.y + 0.5f;
        var z = this.transform.position.z;
        var pos = new Vector3(x, y, z);
        var q = Quaternion.identity;
        if (!used && collision.gameObject.tag == "Player")
        {
            spriteRenderer.color = new Vector4(0, 0.5f, 1, 1);
        }
        if (indicator != null)
        {
            Destroy(indicator);
            indicator = Instantiate(prefab, pos, q);
        }
        else
        {
            indicator = Instantiate(prefab, pos, q);
        }
    }//end OnTriggerEnter2D()

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spriteRenderer.color = Color.white;
        }
        if (indicator != null)
        {
            Destroy(indicator);
        }
    }//end OnTriggerExit2D()
}