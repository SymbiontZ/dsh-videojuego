using UnityEngine;
using UnityEngine.SceneManagement;

public class Volver : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.sceneController.CargarEscenaDelay("Puzzle2");
    }
}
