using System.Collections;
using TMPro;
using UnityEngine;

public class EnseñarDialogo : MonoBehaviour
{
    public GameObject dialogo; // Puedes asignarlo desde el inspector
    public string nombreObjetoTexto; // Nombre exacto del objeto de texto en el Canvas

    private void Start()
    {
        // Si no se asignó desde el inspector, intentar encontrarlo
        if (dialogo == null && !string.IsNullOrEmpty(nombreObjetoTexto))
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                Transform textoTransform = canvas.transform.Find(nombreObjetoTexto);
                if (textoTransform != null)
                {
                    dialogo = textoTransform.gameObject;
                }
                else
                {
                    Debug.LogError($"No se encontró el objeto con nombre: {nombreObjetoTexto}");
                }
            }
            else
            {
                Debug.LogError("No se encontró el Canvas en la escena");
            }
        }

        // Asegurarse que el diálogo empiece desactivado
        if (dialogo != null)
        {
            dialogo.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (dialogo != null)
        {
            StartCoroutine(MostrarDialogos());
        }
        else
        {
            Debug.LogError("El objeto de diálogo no está asignado");
        }
    }

    IEnumerator MostrarDialogos()
    {
        // Mostrar el texto
        dialogo.SetActive(true);
        yield return new WaitForSeconds(5f);

        // Ocultar el texto
        dialogo.SetActive(false);
    }
}