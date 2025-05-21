using TMPro;
using UnityEngine;

public class Jugador : MonoBehaviour
{

    /*** SINGLETON NO TOCAR ***/
    public static Jugador Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(transform.root.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject); //MANTENER JUGADOR ENTRE ESCENAS
    }

    /*** ATRIBUTOS ***/

    public GameObject linterna; // Referencia a la linterna    
    private bool TieneLinterna = false; // Variable para verificar si el jugador tiene la linterna
    private GameObject Luz;
    private Camera jugadorCamara; // Para obtener la cámara del jugador
    public AudioSource Linterna_sound; // Para reproducir sonidos

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linterna.SetActive(false); // Desactiva la linterna al inicio
        Luz = linterna.transform.GetChild(1).gameObject; // Obtiene la luz de la linterna
        jugadorCamara = Camera.main; // Asigna la cámara principal del jugador
    }


    // Update is called once per frame
    void Update()
    {
        // Comprobar si el jugador tiene la linterna y presiona la tecla F
        if (Input.GetKeyDown(KeyCode.F) && TieneLinterna)
        {
            Linterna_sound.Play(); // Reproduce el sonido
            Luz.SetActive(!Luz.activeSelf); // Cambia el estado de la linterna (activa/desactiva)
        }

        while (jugadorCamara == null)
        {
            jugadorCamara = Camera.main; // Asigna la cámara principal del jugador
        }

        // Llamada al método para disparar el rayo y comprobar la distancia
        ComprobarInteraccion();
    }

    // Detecta colisiones con objetos que tienen un collider con Is Trigger activado
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUpLinterna")) // Verifica si el objeto tiene el tag "PickUpLinterna"
        {
            TieneLinterna = true; // El jugador ahora tiene la linterna
            linterna.SetActive(true); // Activa la linterna en la escena
            other.gameObject.SetActive(false); // Desactiva el objeto de la linterna en la escena
        }
        else if (other.CompareTag("Objeto"))
        {
            if (other.GetComponent<AudioAttach>() != null)
            {
                AudioClip audioClip = other.GetComponent<AudioAttach>().ObtenerClip("recoger");
                GameManager.Instance.sfxController.ReproducirSFX(audioClip, other.transform);
            }
            UIManager.Instance.RecogerObjeto();
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Coleccionable"))
        {
            if (other.GetComponent<AudioAttach>() != null)
            {
                AudioClip audioClip = other.GetComponent<AudioAttach>().ObtenerClip("recoger");
                GameManager.Instance.sfxController.ReproducirSFX(audioClip, other.transform);
            }
            UIManager.Instance.RecogerColeccionable();
            other.gameObject.SetActive(false);
        }
    }

    [SerializeField] private float distanciaRayo = 5f;
    [SerializeField] private LayerMask capaInteractuable;
    // Método para lanzar el rayo y comprobar la distancia con las puertas
    private void ComprobarInteraccion()
    {

        Vector3 direccion = jugadorCamara.transform.forward;
        Vector3 origen = jugadorCamara.transform.position;
        Debug.DrawRay(origen, direccion * distanciaRayo, Color.red, 0f, false);

        // Lanza un rayo desde la cámara del jugador hacia donde esté mirando
        if (Physics.Raycast(origen, direccion, out RaycastHit hit, distanciaRayo, capaInteractuable))
        {

            GameManager.Instance.outlineController.AplicarResaltado(hit.collider.gameObject);
            UIManager.Instance.InteractText("Presiona E para interactuar");

            if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.interactionController.Interactuar(hit.collider.gameObject);
            }
        }
        else
        {
            GameManager.Instance.outlineController.QuitarResaltado();
            UIManager.Instance.InteractText("");
        }
    }

    public void SetSpawn(Transform spawn)
    {
        transform.position = spawn.position;
        transform.rotation = spawn.rotation;
        
    }


    public void GameOver()
    {
        UIManager.Instance.ReiniciarPuntos();
        UIManager.Instance.ReiniciarColeccionables();
        GameManager.Instance.sceneController.RecargarEscenaActual();
    }

    public void DesactivarJugador()
    {
        transform.parent.gameObject.SetActive(false);
    }

    public void ActivarJugador()
    {
        transform.parent.gameObject.SetActive(true);
    }
}
