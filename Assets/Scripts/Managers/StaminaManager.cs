using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

[System.Serializable]
public class StaminaManager
{
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float currentStamina = 100f;
    [SerializeField] private float staminaDrainRate = 10f;
    [SerializeField] private float staminaRechargeRate = 5f;
    [SerializeField] private RectTransform staminaImage;

    public Image staminaBar;

    private Vector3 originalScale;
    private Vector3 originalPosition;

    public void InitializeStamina()
    {
        originalScale = staminaImage.localScale;
        originalPosition = staminaImage.localPosition;
    }

    public bool HasStamina() => currentStamina > 0;

    public void DrainStamina(float deltaTime)
    {
        currentStamina -= staminaDrainRate * deltaTime;
        currentStamina = Mathf.Max(currentStamina, 0);
    }

    public void RechargeStamina(float deltaTime)
    {
        currentStamina += staminaRechargeRate * deltaTime;
        currentStamina = Mathf.Min(currentStamina, maxStamina);
    }

    public void UpdateStaminaUI()
    {
        float scaleFactor = Mathf.Clamp(currentStamina / maxStamina, 0f, 1f);
        float targetScaleValue = 2.9f - (scaleFactor * (2.9f - 0.25f));
        staminaImage.localScale = Vector3.Lerp(staminaImage.localScale,
                                                new Vector3(targetScaleValue, targetScaleValue, staminaImage.localScale.z),
                                                Time.deltaTime * 5f);

        Color fullStaminaColor = Color.white;
        Color lowStaminaColor = Color.red;
        staminaImage.GetComponent<Image>().color = Color.Lerp(lowStaminaColor, fullStaminaColor, scaleFactor);

        if (currentStamina < maxStamina * 0.35f)
        {
            ShakeStaminaUI();
        }
        else
        {
            staminaImage.localPosition = originalPosition;
        }
    }

    private void ShakeStaminaUI()
    {
        float shakeMagnitude = 10f;
        float shakeDuration = 0.5f;
        float shakeTime = 0f;

        if (shakeTime < shakeDuration)
        {
            shakeTime += Time.deltaTime;
            float xOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            float yOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            staminaImage.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0);
        }
        else
        {
            shakeTime = 0f;
            staminaImage.localPosition = originalPosition;
        }
    }
}