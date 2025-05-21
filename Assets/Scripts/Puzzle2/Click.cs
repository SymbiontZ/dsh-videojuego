using UnityEngine;

public class Click : MonoBehaviour
{
    public Transform cloneObj;
    public int foodValue;
    private static float currentHeight = 2f;     // Altura base
    private const float heightIncrement = 0.05f; // Incremento por capa

    private void OnMouseDown()
    {
        Vector3 spawnPosition = new Vector3((float)-2.287798, currentHeight, (float)20.3);
        Instantiate(cloneObj, spawnPosition, cloneObj.rotation);

        if (gameObject.CompareTag("Cocinado"))
        {
            // Destruyo el objeto en la sartén
            gameObject.SetActive(false);
            GameFlow.isCooking = false;
        }

        currentHeight += heightIncrement;
        GameFlow.plateValue += foodValue;
        Debug.Log(GameFlow.plateValue + " - " + GameFlow.orderValue);
    }

    // Resetear la altura cuando se completa un pedido
    public static void ResetHeight()
    {
        currentHeight = 2f;
    }
}