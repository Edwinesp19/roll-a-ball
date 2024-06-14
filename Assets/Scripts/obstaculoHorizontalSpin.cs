using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaculoHorizontalSpin : MonoBehaviour
{
    public float speed = 1.5f;
    public float rotationX = 100;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the obstacle continuously

        transform.Rotate(new Vector3(rotationX * Time.deltaTime * speed, 0, 0));
    }
}
