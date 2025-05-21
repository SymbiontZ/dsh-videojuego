using UnityEngine;

public class Cocinar : MonoBehaviour
{
    public Transform cloneObj;

    private void OnMouseDown()
    {
        if (!GameFlow.isCooking &&  // Sólo se cocina si la sartén NO está ocupada
            (gameObject.name == "Bacon_Crudo" || gameObject.name == "Champi_Crudo" || gameObject.name == "Hamborguesa_Cruda"))
        {
            GameFlow.isCooking = true;  // Bloquea la cocina
            Instantiate(cloneObj, new Vector3((float)-3.052, (float)2, (float)20.438), cloneObj.rotation);
        }
    }
}