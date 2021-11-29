using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public bool followPlayer;
    private bool boss;

    // Start is called before the first frame update
    void Start()
    {
        boss = (SceneManager.GetActiveScene().name == "Level_Boss") ? true : false;
        followPlayer = true;
        if (lookAt == null)
        {
            lookAt = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (followPlayer)
        {
            float deltaX = lookAt.position.x - this.transform.position.x;
            float deltaY = lookAt.position.y - this.transform.position.y;
            if (boss) { deltaY += 1f; }
            transform.position += new Vector3(deltaX, deltaY, 0);
        }
    }
}