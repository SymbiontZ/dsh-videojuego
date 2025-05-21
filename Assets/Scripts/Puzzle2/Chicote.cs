using UnityEngine;
using TMPro;
using System.Collections;

public class Chicote : MonoBehaviour
{
    public TextMeshPro primero, segundo;

    private void OnMouseDown()
    {
        // Primero desactivamos ambos textos
        primero.gameObject.SetActive(false);
        segundo.gameObject.SetActive(false);

        // Iniciamos la secuencia de diálogo
        StartCoroutine(MostrarDialogos());
    }

    IEnumerator MostrarDialogos()
    {
        // Mostrar primer texto
        primero.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); // Esperar 3 segundos

        // Ocultar primero y mostrar segundo
        primero.gameObject.SetActive(false);
        segundo.gameObject.SetActive(true);
        yield return new WaitForSeconds(6f); // Esperar otros 3 segundos

        // Ocultar segundo texto al final
        segundo.gameObject.SetActive(false);
    }
}