using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    // Variables de estado del juego
    public static int orderValue, plateValue = 000000000;
    public static int[] menu = { 121010011, 202111111, 101100211, 030200511, 110221011 };
    public static bool isCooking = false;
    public static bool orderAcepted = false;
    public static int aciertos = 4;
    public static int vaciarPlato = -1;

    // UI y tiempo
    public Text cuentaAtras;
    public Text resultadoTexto;
    private float orderTime = 120f, tiempoActual;
    private Coroutine cuentaAtrasCoroutine;
    private bool tiempoAgotado = false;

    // Singleton para fácil acceso
    public static GameFlow Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Click.ResetHeight();
        aciertos = 4;
        isCooking = false;
        orderAcepted = false;
        Time.timeScale = 1f;
    }

    // ----- MÉTODOS DE TIEMPO -----
    public void IniciarCuentaAtras()
    {
        tiempoActual = orderTime;
        cuentaAtrasCoroutine = StartCoroutine(CorutinaCuentaAtras());
    }

    IEnumerator CorutinaCuentaAtras()
    {
        while (tiempoActual > 0 && !tiempoAgotado)
        {
            cuentaAtras.text = "Tiempo restante: " + Mathf.Round(tiempoActual).ToString();
            yield return new WaitForSeconds(1f);
            tiempoActual--;
        }

        if (!tiempoAgotado && tiempoActual <= 0)
        {
            TiempoAgotado();
        }
    }

    public void DetenerCuentaAtras()
    {
        if (cuentaAtrasCoroutine != null)
        {
            StopCoroutine(cuentaAtrasCoroutine);
        }
    }

    // ----- MÉTODOS DE RESULTADO -----
    public void TiempoAgotado()
    {
        tiempoAgotado = true;
        DetenerCuentaAtras();
        MostrarResultado(false); // false = derrota
    }

    public void PedidoCorrecto()
    {
        aciertos--;
        if (aciertos <= 0)
        {
            MostrarResultado(true); // true = victoria
        }
    }

    private void MostrarResultado(bool victoria)
    {
        resultadoTexto.text = victoria ? "¡VICTORIA!" : "¡TIEMPO AGOTADO!";
        if (resultadoTexto.text == "¡TIEMPO AGOTADO!")
        {
            ReiniciarJuego();
        }
        else
        {
            GameManager.Instance.sceneController.CargarEscenaDelay("Nivel2_Alter");
        }
    }

    // ----- MÉTODOS DE UI -----
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene("Puzzle2");
    }
}