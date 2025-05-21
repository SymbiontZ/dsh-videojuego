using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class StaminaUI : MonoBehaviour
{
    public Slider staminaSlider;
    public FirstPersonController player;

    void Start()
    {
        if (player == null && Jugador.Instance != null)
        {
            player = Jugador.Instance.GetComponent<FirstPersonController>();
        }

        staminaSlider.maxValue = player.MaxStamina;
        staminaSlider.value = player.CurrentStamina;
    }

    void Update()
    {
        staminaSlider.value = player.CurrentStamina;
    }
}
