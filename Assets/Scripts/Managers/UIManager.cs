using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /*** SINGLETON NO TOCAR ***/
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Si lo deseas persistente entre escenas
    }

    /*** ATRIBUTOS ***/

    [Header("Textos")]

    public TMP_Text interactText;
    public TMP_Text objectIndicator;
    public TMP_Text puntosIndicator;
    public TMP_Text mensajeText;

    public Image panelPausa;

    [Header("Sliders")]

    public Slider staminaSlider;



    /*** STAMINA ***/
    private FirstPersonController jugador;

    /*** COLECCIONABLES ***/
    [SerializeField] private int objetos;

    [SerializeField] private int coleccionables;

    [SerializeField] private int coleccionablesOld;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Jugador.Instance != null && jugador == null)
        {
            jugador = Jugador.Instance.GetComponent<FirstPersonController>();
            staminaSlider.maxValue = jugador.MaxStamina;
            staminaSlider.value = jugador.CurrentStamina;
        }
        objetos = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if (jugador != null)
        {
            staminaSlider.value = jugador.CurrentStamina;
        }


    }

    public void InteractText(string texto)
    {
        interactText.text = texto;
    }

    private void actualizarColeccionable()
    {
        objectIndicator.text = "Objetos recogidos: " + coleccionables;
    }
    private void actualizarObjeto()
    {
        puntosIndicator.text = "Puntuación: " + objetos;
    }

    public void RecogerColeccionable()
    {
        coleccionables += 1;
        actualizarColeccionable();
    }
    public void RecogerObjeto()
    {
        objetos += 1;
        actualizarObjeto();
    }

    public int ObtenerPuntos()
    {
        return objetos;
    }
    public int ObtenerColeccionables()
    {
        return coleccionables;
    }

    public void ReiniciarPuntos()
    {
        objetos = 0;
        actualizarObjeto();
    }
    public void ReiniciarColeccionables()
    {
        coleccionables = coleccionablesOld;
        actualizarColeccionable();
    }
    public void GuardarColeccionables()
    {
        coleccionablesOld = coleccionables;
    }


    public void MostrarMensaje(string mensaje, float tiempo = 2f)
    {
        mensajeText.text = mensaje;
        Invoke("OcultarMensaje", tiempo); // Oculta el mensaje después de 2 segundos
    }
    private void OcultarMensaje()
    {
        mensajeText.text = "";
    }

    public void Desactivar()
    {
        gameObject.SetActive(false);
    }
    public void Activar()
    {
        gameObject.SetActive(true);

        if (panelPausa != null)
        {
            panelPausa.gameObject.SetActive(false);
        }
    }

}
