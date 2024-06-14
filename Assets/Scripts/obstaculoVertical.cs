using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaculoVertical : MonoBehaviour
{
    public float startY = -4.6f; // Starting position on the Z-axis
    public float endY = 0.35f; // Ending position on the Z-axis 
    public float speed = 4f;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.PingPong(Time.time * speed, endY - startY) + startY;
        Vector3 newPosition = new Vector3(transform.position.x, newY, transform.position.z);
        transform.position = newPosition;
    }
}
