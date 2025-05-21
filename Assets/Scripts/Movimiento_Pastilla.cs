using UnityEngine;

public class LevitateAndRotate : MonoBehaviour
{
    public float levitationAmplitude = 0.5f; // Qué tan alto sube y baja
    public float levitationSpeed = 2f;       // Velocidad del movimiento de levitación
    public float rotationSpeed = 50f;        // Velocidad de rotación en grados por segundo

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Levitar: usar seno para oscilación suave
        float newY = startPosition.y + Mathf.Sin(Time.time * levitationSpeed) * levitationAmplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // Rotar alrededor del eje Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
