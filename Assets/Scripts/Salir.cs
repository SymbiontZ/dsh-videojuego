using UnityEngine;
using UnityEngine.SceneManagement;

public class Salir : MonoBehaviour
{
    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo de la aplicación..."); // Esto solo se ve en el editor
    }
}

