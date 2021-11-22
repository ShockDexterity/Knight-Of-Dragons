using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public bool followPlayer;

    // Start is called before the first frame update
    void Start()
    {
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
            float deltaY = lookAt.position.y - this.transform.position.y + 1f;
            transform.position += new Vector3(deltaX, deltaY, 0);
        }
    }
}