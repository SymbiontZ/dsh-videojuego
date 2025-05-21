using UnityEngine;
using StarterAssets;

//CODIGO EXCLUSIVO DEL OBJETI LINTERNA, NO TOCAR

public class FlashlightSway : MonoBehaviour
{
    [Header("Mouse Movement")]
    [SerializeField] private float mouseSwayAmount = 0.5f;
    [SerializeField] private float mouseSwaySmooth = 6f;
    [SerializeField] private float mouseTiltAmount = 2f;
    [SerializeField] private float mouseTiltSmooth = 8f;

    [Header("WASD Movement (Vertical Only)")]
    [SerializeField] private float verticalSwayAmount = 0.1f;  // Intensidad del movimiento vertical
    [SerializeField] private float swaySpeed = 3f;             // Velocidad de oscilación
    [SerializeField] private float runMultiplier = 1.5f;       // Aumento al correr

    [Header("Smoothing")]
    [SerializeField] private float returnSpeed = 5f;

    [SerializeField] private FirstPersonController playerMovement;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector2 mouseInput;
    private float movementTimer;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // --- Inputs ---
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        bool isRunning = playerMovement.IsActuallyRunning;

        // --- Mouse Sway ---
        Vector3 mouseSway = new Vector3(
            -mouseInput.x * mouseSwayAmount,
            -mouseInput.y * mouseSwayAmount * 0.5f,
            0
        );

        // --- Mouse Tilt ---
        float mouseTiltZ = -mouseInput.x * mouseTiltAmount;

        // --- WASD Movement (Pure Vertical) ---
        if (isMoving)
        {
            movementTimer += Time.deltaTime * swaySpeed;
            float currentSway = verticalSwayAmount * (isRunning ? runMultiplier : 1f);
            float swayY = Mathf.Sin(movementTimer) * currentSway; // Oscilación vertical para todos los inputs

            mouseSway += new Vector3(0, swayY, 0);
        }
        else
        {
            movementTimer = 0;
        }

        // --- Apply Movement ---
        Vector3 targetPosition = initialPosition + mouseSway;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, 0, mouseTiltZ);

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * (isMoving ? mouseSwaySmooth : returnSpeed)
        );
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * mouseTiltSmooth
        );
    }
}