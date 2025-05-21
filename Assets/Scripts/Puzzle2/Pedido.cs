using System.Collections;
using UnityEngine;

public class Pedido : MonoBehaviour
{
    public UnityEngine.UI.Text panA, tomate, lechuga, cebolla, queso, champi, bacon, carne, panB;
    private GameFlow gameFlow;
    public AudioSource incorrecto;
    private AudioSource correcto;

    void Start()
    {
        correcto = GetComponent<AudioSource>();
        gameFlow = GameObject.Find("GM").GetComponent<GameFlow>();
    }

    private void OnMouseDown()
    {
        if (GameFlow.orderValue == GameFlow.plateValue)
        {
            correcto.Play();
            Debug.Log("Correcto!");
            GameFlow.Instance.DetenerCuentaAtras();

            // Limpiar UI
            ResetearTextos();

            GameFlow.Instance.PedidoCorrecto(); // Verifica si ganó
            GameFlow.orderAcepted = false;
        }
        else
        {
            incorrecto.Play();
            Debug.Log("Inténtalo de nuevo.");
        }

        GameFlow.vaciarPlato = 1;
        StartCoroutine(Esperar());
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(2f);
        Click.ResetHeight();
        GameFlow.plateValue = 000000000;
        GameFlow.vaciarPlato = -1;
    }

    private void ResetearTextos()
    {
        panA.text = "X 0";
        tomate.text = "X 0";
        lechuga.text = "X 0";
        cebolla.text = "X 0";
        queso.text = "X 0";
        champi.text = "X 0";
        bacon.text = "X 0";
        carne.text = "X 0";
        panB.text = "X 0";
    }
}