using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private IResaltable actualResaltado;

    public void AplicarResaltado(GameObject objeto)
    {
        if (actualResaltado != null && actualResaltado != objeto.GetComponent<IResaltable>())
        {
            actualResaltado.DesactivarResaltado();
        }

        IResaltable resaltable = objeto.GetComponent<IResaltable>();
        if (resaltable != null && resaltable != actualResaltado)
        {
            resaltable.ActivarResaltado();
            actualResaltado = resaltable;
        }
    }

    public void QuitarResaltado()
    {
        if (actualResaltado != null)
        {
            actualResaltado.DesactivarResaltado();
            actualResaltado = null;
        }
    }
}

