using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class PauseController : MonoBehaviour
{
    public GameObject panelPausa;
    private bool juegoPausado = false;

    private FirstPersonController playerController;

    void Start()
    {
        if (playerController == null)
            playerController = Jugador.Instance.GetComponent<FirstPersonController>();

        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    void Update()
    {
        // Si se pulsa Esc o el botón de pausa en el panel de la UI, se pausa o reanuda el juego
        if (Input.GetKeyDown(KeyCode.Escape) && panelPausa != null)
        {
            if (juegoPausado)
                Reanudar();
            else
                Pausar();
        }
    }

    public void Pausar()
    {
        panelPausa.SetActive(true);  // Muestra el panel de pausa
        Time.timeScale = 0f;         // Congela el juego
        juegoPausado = true;

        playerController.enabled = false; // Desactiva el controlador del jugador
        Cursor.lockState = CursorLockMode.None; // Libera el cursor
        Cursor.visible = true; // Muestra el cursor
    }

    public void Reanudar()
    {
        panelPausa.SetActive(false); // Oculta el panel de pausa
        Time.timeScale = 1f;         // Reanuda el juego
        juegoPausado = false;

        playerController.enabled = true; // Reactiva el controlador del jugador
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
        Cursor.visible = false; // Oculta el cursor
    }

    public void SalirDelJuego()
    {
        Application.Quit(); // Sale del juego
    }

    public void IrAlMenuPrincipal()
    {
        // Reinicia el juego y carga la escena del menú principal
        Time.timeScale = 1f; // Asegúrate de que el tiempo esté normalizado
        juegoPausado = false; // Reinicia el estado de pausa
        SceneManager.LoadScene("Menu");
    }
    
    
}