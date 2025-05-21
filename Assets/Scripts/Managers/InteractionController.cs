using System.Collections;
using DoorScript;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour
{

    public void Interactuar(GameObject objeto)
    {
        string tagObjeto = objeto.tag;
        Debug.Log("Interacción con objeto: " + tagObjeto);

        switch (tagObjeto)
        {
            case "Puerta":
                Debug.Log("Interacción: abrir puerta");

                Door puerta = objeto.GetComponent<Door>();

                if (puerta != null)
                {
                    puerta.OpenDoor();
                }
                break;

            case "Linterna":
                Debug.Log("Interacción: recoger linterna");
                break;

            case "GoodEndTrigger":
                GameManager.Instance.sceneController.CargarEscenaDelay("GoodEnding");
                break;

            case "Tamagochi":
                objeto.layer = LayerMask.NameToLayer("Default");
                FindObjectOfType<DialogTrigger>().TriggerDialogue();
                break;

            case "Ascensor":
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Pasillo":
                        GameManager.Instance.sceneController.CargarEscenaDelay("Nivel1");
                        break;

                    case "Nivel1_Alter":
                        GameManager.Instance.sceneController.CargarEscenaDelay("Nivel2");
                        break;

                    case "Nivel2_Alter":
                        if (UIManager.Instance.ObtenerColeccionables() >= 5)
                        {
                            GameManager.Instance.sceneController.CargarEscenaDelay("GoodEnding");
                        }
                        else
                        {
                            GameManager.Instance.sceneController.CargarEscenaDelay("BadEnding");
                        }
                        break;

                    default:
                        Debug.Log("No se ha podido cambiar de escena");
                        break;
                }
                break;

            case "Recreativa":
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Nivel1":
                        GameManager.Instance.sceneController.CargarEscenaDelay("Puzzle1");
                        break;

                    case "Nivel2":
                        GameManager.Instance.sceneController.CargarEscenaDelay("Puzzle2");
                        break;
                    default:
                        Debug.Log("No se ha podido cambiar de escena");
                        break;
                }
                break;

            case "Finish":
                GameManager.Instance.sceneController.CargarEscenaDelay("Credits");
                break;

            default:
                Debug.Log("Interacción no definida para tag: " + tagObjeto);
                break;

        }
    }
    
    
}
