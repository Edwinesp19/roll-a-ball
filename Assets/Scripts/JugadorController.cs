using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class JugadorController : MonoBehaviour
{
    private Rigidbody rb; //variable de tipo Rigidbody que asociaremos a nuestro jugador

    public AudioClip sonidoAlGanar; //efecto de sonido al ganar
    public AudioClip sonidoAlPerder; //efecto de sonido al perder
    public AudioClip sonidoPocoTiempo; //efecto de sonido cuando queda poco tiempo
    public AudioClip sonidoTiempoAcabado; //efecto de sonido cuando se acaba el tiempo

    public AudioClip sonidoColeccionable;

    public AudioSource quienEmite; //variable de tipo AudioSource que asociaremos a nuestro jugador

    private int contador; // inizializamos el contador de los coleccionables recogidos

    public Text textoContador, textoGanar, textoTiempo; //Inicializo variables para los textos

    //Declaro la variable pública velocidad para poder modificarla desde la Inspector window
    public float velocidad;

    public List<GameObject> coleccionablesDesactivados; //lista de coleccionables desactivados

    float time = 60f; //tiempo
    public float levelTime; //tiempo



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //capturo la variable al iniciar el juego

        contador = 0; //inicializo el contador en 0 
        setTextoContador(); //Actualizo el texto del contador por pimera vez
        textoGanar.text = ""; //Inicializo el texto de ganar en vacío
        time = levelTime;
    }

    void FixedUpdate()
    {

        //variables que capturan el movimiento del jugador en el eje horizontal y vertical de nuestro teclado
        float movimientoH = Input.GetAxis("Horizontal");
        float movimientoV = Input.GetAxis("Vertical");

        // un vector 3 es un trío de posiciones en el espacio XYZ 
        Vector3 movimiento = new Vector3(movimientoH, 0.0f, movimientoV);

        //asigno el movimiento o desplazamiento a mi RigidBody
        rb.AddForce(movimiento * velocidad);

        //para manejar el tiempo
        if (time > 0)
        {
            time -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            textoTiempo.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        if (time < 20)
        {
            quienEmite.pitch = 1.5f;
        }
        else
        {
            quienEmite.pitch = 1f;
        }

        if (time <= 0)
        {
            quienEmite.PlayOneShot(sonidoAlPerder, 1f);
            restartLevel();
        }


    }

    void restartLevel()
    {
        //descongelar las teclas del teclado
        rb.constraints = RigidbodyConstraints.None;
        contador = 0;

        time = levelTime;
        setTextoContador();

        if (coleccionablesDesactivados.Count > 0)
        {
            foreach (GameObject coleccionable in coleccionablesDesactivados)
            {
                coleccionable.SetActive(true);
            }

        }

        //cambiar la posición del jugador

        switch (SceneManager.GetActiveScene().name)
        {
            case "nivel-1":
                transform.position = new Vector3(0, 0.5f, 0);
                break;
            case "nivel-2":
                transform.position = new Vector3(0, 0.5f, 0);
                break;
            case "nivel-3":
                transform.position = new Vector3(-7.02f, 0.79f, -8);
                break;
            case "nivel-4":
                transform.position = new Vector3(7.64f, 0.79f, -2.61f);
                break;
            case "nivel-5":
                transform.position = new Vector3(-7.02f, 0.79f, -8);
                break;
            case "nivel-6":
                transform.position = new Vector3(-7.02f, 0.79f, -8);
                break;
            default:
                CambiarDeNivel("MenuInicio");
                break;
        }
    }

    //Se ejecuta al entrar a un objeto con la opción isTrigger seleccionada
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            other.gameObject.SetActive(false);
            quienEmite.PlayOneShot(sonidoColeccionable, 1f);
            contador = contador + 1;
            setTextoContador(); // actualizo el texto del contador
            coleccionablesDesactivados.Add(other.gameObject); //agrego el coleccionable a la lista de coleccionables desactivados
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstaculo"))
        {
            quienEmite.PlayOneShot(sonidoAlPerder, 1f);
            restartLevel();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void setTextoContador()
    {
        textoContador.text = "Contador: " + contador.ToString();

        if (contador >= 12)
        {
            textoGanar.text = "¡Ganaste!";
            quienEmite.PlayOneShot(sonidoAlGanar, 1f);


            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;


            switch (SceneManager.GetActiveScene().name)
            {
                case "nivel-1":
                    CambiarDeNivel("nivel-2");
                    break;

                case "nivel-2":
                    CambiarDeNivel("nivel-3");
                    break;

                case "nivel-3":
                    CambiarDeNivel("nivel-4");
                    break;

                case "nivel-4":
                    CambiarDeNivel("nivel-5");
                    break;

                case "nivel-5":
                    CambiarDeNivel("nivel-6");
                    break;

                case "nivel-6":
                    CambiarDeNivel("MenuInicio");
                    break;

                default:
                    CambiarDeNivel("MenuInicio");
                    break;
            }
        }
    }

    public void CambiarDeNivel(string escena)
    {
        StartCoroutine(CambiarEscena(escena, 3f));
    }

    IEnumerator CambiarEscena(string escena, float delay)
    {
        //esperar el delay antes de ir a la escena
        yield return new WaitForSeconds(delay);
        //cargar la escena
        SceneManager.LoadScene(escena);
        restartLevel();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
