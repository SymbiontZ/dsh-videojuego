using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuBoton: MonoBehaviour
{
    public string nombreEscena;

    public void CargarEscena()
    {
        StartCoroutine(EsperarYcargar());
    }

    private IEnumerator EsperarYcargar()
    {
        // Espera hasta que GameManager.Instance y sceneController no sean null
        while (GameManager.Instance == null || GameManager.Instance.sceneController == null)
        {
            yield return null; // Espera un frame
        }
        GameManager.Instance.sceneController.CargaEscena(nombreEscena);
    }
}
