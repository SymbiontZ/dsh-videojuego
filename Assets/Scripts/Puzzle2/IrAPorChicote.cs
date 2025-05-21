using UnityEngine;
using UnityEngine.SceneManagement;

public class IrAPorChicote : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.sceneController.CargarEscenaDelay("Chicote");
    }
}