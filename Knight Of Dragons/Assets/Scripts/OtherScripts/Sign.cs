using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public SignText signText;
    public int id;
    public bool canClick;

    // Start is called before the first frame update
    void Start()
    {
        if (signText == null) { signText = GameObject.Find("SignText").GetComponent<SignText>(); }
        canClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canClick && Input.GetKeyDown(KeyCode.E))
        {
            signText.ShowText(id);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canClick = true;
        }
    }//end OnTriggerEnter2D()

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canClick = false;
            signText.HideText();
        }
    }//end OnTriggerExit2D()
}