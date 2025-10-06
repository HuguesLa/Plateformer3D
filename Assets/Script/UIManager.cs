using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider staminaSlider;
    public PlayerController playerController;

    void Start()
    {
        if (staminaSlider == null)
        {
            Debug.LogError("Stamina Slider is not assigned in UIManager!");
        }

        if (playerController == null)
        {
            Debug.LogError("PlayerController is not assigned in UIManager!");
        }
    }

    void Update()
    {
        if (staminaSlider != null && playerController != null)
        {
            staminaSlider.value = playerController.GetStaminaPercentage();
        }
    }
}