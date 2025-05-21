using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoreScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.1f; // Velocidad de scroll
    private bool isScrolling = true;
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    void Update()
    {
        if (isScrolling)
        {
            // Scroll automático hacia arriba
            scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;

            // Si llega al final
            if (scrollRect.verticalNormalizedPosition <= 0f)
            {
                isScrolling = false;
                LoadNextScene();
            }
        }

        // Si el jugador presiona una tecla para saltar
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        if(SceneManager.GetActiveScene().name == "Lore")
        {
            GameManager.Instance.sceneController.CargaEscena("Pasillo");
        }
        else if(SceneManager.GetActiveScene().name == "Credits")
        {
            GameManager.Instance.sceneController.CargaEscena("Menu");
        }
    }
}
