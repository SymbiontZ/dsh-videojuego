using UnityEngine;

public class Tirar : MonoBehaviour
{
    void Update()
    {
        if (GameFlow.vaciarPlato == 1)
        {
            Destroy(gameObject);
        }
    }
}
