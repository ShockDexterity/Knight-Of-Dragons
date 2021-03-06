using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoor : MonoBehaviour
{
    public bool canEnter;
    private string scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
        canEnter = false;
    }//end Start()

    // Update is called once per frame
    void Update()
    {
        if (canEnter && Input.GetKeyDown(KeyCode.E))
        {
            switch (scene)
            {
                case "Level_0":
                    SceneManager.LoadScene("Level_1");
                    break;

                case "Level_1":
                    SceneManager.LoadScene("Level_2");
                    break;

                case "Level_2":
                    SceneManager.LoadScene("Level_3");
                    break;

                case "Level_3":
                    SceneManager.LoadScene("Level_Boss");
                    break;
            }
        }
    }//end Update()

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canEnter = true;
        }
    }//end OnTriggerEnter2D()

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canEnter = false;
        }
    }//end OnTriggerExit2D()
}
