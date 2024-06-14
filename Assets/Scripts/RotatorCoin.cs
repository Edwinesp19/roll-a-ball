using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorCoin : MonoBehaviour
{
    public AudioSource quienEmite;
    public AudioClip sonidoColeccionable;



    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //Rota el elemento una cantidad diferente en cada dirección y encada intervalo de tiempo
        transform.Rotate(new Vector3(100 * Time.deltaTime, 0, 0));
    }
}
