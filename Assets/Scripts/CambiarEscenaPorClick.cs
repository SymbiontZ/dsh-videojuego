using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenaPorClick : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Finish"))
                {
                    GameManager.Instance.sceneController.CargaEscena("Credits");
                    //GestorCarga.IrAEscenaConCarga("Credits");
                }
            }
        }
    }
}
