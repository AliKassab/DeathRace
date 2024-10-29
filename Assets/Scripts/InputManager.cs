using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityTutorial.Manager
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }
        public bool Run { get; private set; }

        public bool TelepathyMode { get; private set; }
        public bool Interaction { get; private set; }
        public bool Close {  get; private set; }

        private InputActionMap currentMap;
        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction runAction;
        private InputAction telepathyMode;
        private InputAction interaction;
        private InputAction closeAction;

        private bool telepathyPressedThisFrame;
        private bool interactionPressedThisFrame;
        private bool closePressedThisFrame;

        private void Awake()
        {
            currentMap = playerInput.currentActionMap;
            moveAction = currentMap.FindAction("Move");
            lookAction = currentMap.FindAction("Look");
            runAction = currentMap.FindAction("Run");
            telepathyMode = currentMap.FindAction("TelepathyMode");
            interaction = currentMap.FindAction("Interaction");
            closeAction = currentMap.FindAction("Close");

            moveAction.performed += onMove;
            lookAction.performed += onLook;
            runAction.performed += onRun;
            telepathyMode.performed += onTelepathyMode;
            interaction.performed += onInteraction;
            closeAction.performed += onClose;

            moveAction.canceled += onMove;
            lookAction.canceled += onLook;
            runAction.canceled += onRun;
            telepathyMode.canceled -= onTelepathyMode;
            interaction.canceled -= onInteraction;
            closeAction.canceled += onClose;

        }

        private void onMove(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        private void onLook(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void onRun(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }

        private void onTelepathyMode(InputAction.CallbackContext context)
        {
            TelepathyMode = true;
            telepathyPressedThisFrame = true;
        }

        private void onInteraction(InputAction.CallbackContext context)
        {
            Interaction = true;
            interactionPressedThisFrame = true;
        }

        private void onClose(InputAction.CallbackContext context)
        {
            Close = true;
            closePressedThisFrame = true;
        }

        private void OnEnable()
        {
            currentMap.Enable();
        }

        private void OnDisable()
        {
            currentMap.Disable();
        }

        public bool IsTelepathyPressedThisFrame()
        {
            if (telepathyPressedThisFrame)
            {
                telepathyPressedThisFrame = false;
                return true;
            }
            return false;
        }

        public bool IsInteractionPressedThisFrame()
        {
            if (interactionPressedThisFrame)
            {
                interactionPressedThisFrame = false;
                return true;
            }
            return false;
        }

        public bool IsClosePressedThisFrame()
        {
            if (closePressedThisFrame)
            {
                closePressedThisFrame = false;
                return true;
            }
            return false;
        }
    }
}
