using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaculoHorizontal : MonoBehaviour
{

    public bool Horizontal = true;
    public float speed = 5f;
    public float startX = -7.06f; // Starting position on the X-axis
    public float endX = -1.01f; // Ending position on the X-axis 
    public float startZ = -8f; // Starting position on the Z-axis
    public float endZ = -4f; // Ending position on the Z-axis 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Horizontal)
        {
            float newX = Mathf.PingPong(Time.time * speed, endX - startX) + startX;
            Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);
            transform.position = newPosition;
        }
        else
        {
            float newZ = Mathf.PingPong(Time.time * speed, endZ - startZ) + startZ;
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, newZ);
            transform.position = newPosition;

        }
    }

}
