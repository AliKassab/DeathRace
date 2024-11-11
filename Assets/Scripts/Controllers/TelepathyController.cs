using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace UnityTutorial.Manager
{
    public class TelepathyController : MonoBehaviour
    {
        #region Inspector
        [Header("Visuals")]
        [SerializeField] private PostProcessProfile originalProfile;
        [SerializeField] private PostProcessProfile telepathyProfile;
        [SerializeField] private PostProcessVolume volume;

        [Header("Stamina")]
        [SerializeField] private StaminaManager staminaManager;

        [Header("Shake Effect")]
        [SerializeField] private IDN_ShakeEffect shakeEffect;

        [Header("Camera Shake")]
        [SerializeField] private CameraShake cameraShake;
        [SerializeField] private float cameraShakeDuration = 0.5f;
        [SerializeField] private float cameraShakeMagnitude = 0.1f;
        #endregion

        #region private members
        private InputManager inputManager;
        private bool isTelepathyActive = false;
        #endregion

        private void Start()
        {
            inputManager = GetComponent<InputManager>();
            staminaManager.InitializeStamina();
        }

        private void Update()
        {
            HandleInput();
            staminaManager.UpdateStaminaUI();
        }

        private void HandleInput()
        {
            if (inputManager.IsTelepathyPressedThisFrame())
            {
                if (!isTelepathyActive && staminaManager.HasStamina())
                    ActivateTelepathy();
                else
                    DeactivateTelepathy();
            }

            if (isTelepathyActive)
            {
                staminaManager.DrainStamina(Time.deltaTime);
                if (!staminaManager.HasStamina())
                {
                    DeactivateTelepathy();
                }
            }
            else
            {
                staminaManager.RechargeStamina(Time.deltaTime);
            }
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
    }
}