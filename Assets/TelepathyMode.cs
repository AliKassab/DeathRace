using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace UnityTutorial.Manager
{
    public class TelepathyMode : MonoBehaviour
    {
        private InputManager inputManager;
        [Header("Visuals")]
        [SerializeField] private PostProcessProfile originalProfile;
        [SerializeField] private PostProcessProfile telepathyProfile;
        [SerializeField] private PostProcessVolume volume;

        [Header("Stamina")]
        [SerializeField] private float maxStamina = 100f;
        [SerializeField] private float currentStamina = 100f;
        [SerializeField] private float staminaDrainRate = 10f;
        [SerializeField] private float staminaRechargeRate = 5f;
        [SerializeField] private RectTransform staminaImage;

        private Vector3 originalScale;
        private Vector3 originalPosition;
        private bool isTelepathyActive = false;

        [Header("Shake Effect")]
        [SerializeField] private float shakeMagnitude = 10f;
        [SerializeField] private float shakeDuration = 0.5f;
        private float shakeTime = 0f;

        [Header("Camera Shake")]
        [SerializeField] private CameraShake cameraShake;
        [SerializeField] private float cameraShakeDuration = 0.5f;
        [SerializeField] private float cameraShakeMagnitude = 0.1f;

        private void Start()
        {
            inputManager = GetComponent<InputManager>();
            originalScale = staminaImage.localScale;
            originalPosition = staminaImage.localPosition;
        }

        private void Update()
        {
            if (inputManager.IsTelepathyPressedThisFrame())
            {
                if (!isTelepathyActive && currentStamina > 0)
                    ActivateTelepathy();
                else
                    DeactivateTelepathy();
            }

            if (isTelepathyActive)
            {
                DrainStamina(Time.deltaTime);
                if (currentStamina <= 0)
                {
                    DeactivateTelepathy();
                }
            }
            else
            {
                RechargeStamina(Time.deltaTime);
            }

            UpdateStaminaUI();
        }

        private void ActivateTelepathy()
        {
            isTelepathyActive = true;
            StartCoroutine(cameraShake.Shake(cameraShakeDuration, cameraShakeMagnitude, telepathyProfile, volume));

            
        }

        private void DeactivateTelepathy()
        {
            isTelepathyActive = false;
            StartCoroutine(cameraShake.Shake(cameraShakeDuration, cameraShakeMagnitude, originalProfile, volume));        
        }

        private void DrainStamina(float deltaTime)
        {
            currentStamina -= staminaDrainRate * deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0);
        }

        private void RechargeStamina(float deltaTime)
        {
            if (!isTelepathyActive && currentStamina < maxStamina)
            {
                currentStamina += staminaRechargeRate * deltaTime;
                currentStamina = Mathf.Min(currentStamina, maxStamina);
            }
        }

        private void UpdateStaminaUI()
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

        public float GetCurrentStamina() => currentStamina;
    }
}
