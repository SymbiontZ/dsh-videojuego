using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    public static string escenaPorCargar;
    public Slider barraProgreso;

    public Canvas canvasCarga;

    public void CargarEscenaDelay(string nombreEscena, float tiempo = 3f)
    {
        escenaPorCargar = nombreEscena;
        UIManager.Instance.Desactivar();
        
        StartCoroutine(CargarEscenaDelayEnum(escenaPorCargar, tiempo));
    }
    
    IEnumerator CargarEscenaDelayEnum(string nombreEscena, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        StartCoroutine(CargarEscenaAsincrona(nombreEscena));
    }


    public void CargaEscena(string escena)
    {
        UIManager.Instance.Desactivar();
        escenaPorCargar = escena;
        StartCoroutine(CargarEscenaAsincrona(escena));
    }

    public void RecargarEscenaActual()
    {
        UIManager.Instance.Desactivar();
        escenaPorCargar = SceneManager.GetActiveScene().name;
        StartCoroutine(CargarEscenaAsincrona(escenaPorCargar));
    }

    IEnumerator CargarEscenaAsincrona(string nombreEscena)
    {
        UIManager.Instance.GuardarColeccionables();
        canvasCarga.gameObject.SetActive(true);

        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);
        barraProgreso.value = 0f;
        
        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f); // Normaliza el valor
            if (barraProgreso != null)
                barraProgreso.value = progreso;

            yield return null;
        }

        canvasCarga.gameObject.SetActive(false);
        GameManager.Instance.DormirSingletons();
        GameManager.Instance.SetCursorEstado();

        yield return null; //Esperar un frame para pillar tag

        GameManager.Instance.SetSpawnJugador();
    }

    public string ObtenerNombreEscenaActual()
    {

        Scene escenaActual = SceneManager.GetActiveScene();
        return escenaActual.name;
    }


}
