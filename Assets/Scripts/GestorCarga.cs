using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorCarga : MonoBehaviour
{
    public static string escenaPorCargar;

    public static void IrAEscenaConCarga(string nombreEscena)
    {
        escenaPorCargar = nombreEscena;
        SceneManager.LoadScene("PantallaCarga");
    }
}

