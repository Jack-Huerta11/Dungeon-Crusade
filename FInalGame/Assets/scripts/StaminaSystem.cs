using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    public float maxStamina = 100.0f;
    public float currentStamina = 100.0f;
    public float staminaRegenRate = 10.0f; // Stamina regeneration rate per second
    public Image staminaBar;

    private bool isExhausted = false;

    private void Start()
    {
        UpdateStaminaBar();
    }

    private void Update()
    {
        // Check if stamina is not at its maximum and not exhausted
        if (currentStamina < maxStamina && !isExhausted)
        {
            // Regenerate stamina over time
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0.0f, maxStamina);
            UpdateStaminaBar();
        }

        // Check for input to spend stamina
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            SpendStamina(25.0f);
        }
    }

    public void SpendStamina(float amount)
    {
        if (!isExhausted && currentStamina >= amount)
        {
            currentStamina -= amount;
            UpdateStaminaBar();
        }
        else
        {
            isExhausted = true; // Stamina is exhausted
        }
    }

    public void StopExhaustion()
    {
        isExhausted = false;
    }

    private void UpdateStaminaBar()
    {
        float staminaRatio = currentStamina / maxStamina;
        staminaBar.fillAmount = staminaRatio;
    }
}
