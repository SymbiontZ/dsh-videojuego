using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("MANAGERS | NO TOCAR NI AGRREGAR")]
    public SceneController sceneController;
    public InteractionController interactionController;
    public OutlineController outlineController;
    public SFXController sfxController;


    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // Si ya existe una instancia y no somos nosotros, destruir este objeto
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asignar la instancia y marcar para no destruir
        Instance = this;

        sceneController = GetComponent<SceneController>();
        interactionController = GetComponent<InteractionController>();
        outlineController = GetComponent<OutlineController>();
        sfxController = GetComponent<SFXController>();

        DontDestroyOnLoad(gameObject);

    }

    public void Start()
    {
        DormirSingletons();
        SetSpawnJugador();
        Cursor.lockState = CursorLockMode.None;
    }

    public void DormirSingletons()
    {
        string escenaActual = sceneController.ObtenerNombreEscenaActual();

        switch (escenaActual)
        {
            case "Menu":
            case "Puzzle1":
            case "Puzzle2":
            case "Credits":
            case "Lore":
            case "Chicote":
                if (Jugador.Instance != null) Jugador.Instance.DesactivarJugador();
                if (UIManager.Instance != null) UIManager.Instance.Desactivar();
                Debug.Log("Desactivando Jugador y UI");
                break;
            default:
                if (Jugador.Instance != null) Jugador.Instance.ActivarJugador();
                if (UIManager.Instance != null) UIManager.Instance.Activar();
                Debug.Log("Activando Jugador y UI");
                break;
        }
    }

    public void SetCursorEstado()
    {
        string escenaActual = sceneController.ObtenerNombreEscenaActual();

        switch (escenaActual)
        {
            case "Menu":
            case "Credits":
            case "Lore":
            case "Puzzle1":
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            default:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }


    public void SetSpawnJugador()
    {
        GameObject spawn = GameObject.FindGameObjectWithTag("Spawn");
        if (spawn != null)
        {
            Jugador.Instance.SetSpawn(spawn.transform);
        }
        else
        {
            Debug.Log("No se encontró spawn");
        }
    }

}
