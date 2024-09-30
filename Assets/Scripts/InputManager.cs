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
        public bool DoorTrigger { get; private set; }

        private InputActionMap currentMap;
        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction runAction;
        private InputAction telepathyMode;

        private bool telepathyPressedThisFrame;

        private void Awake()
        {
            currentMap = playerInput.currentActionMap;
            moveAction = currentMap.FindAction("Move");
            lookAction = currentMap.FindAction("Look");
            runAction = currentMap.FindAction("Run");
            telepathyMode = currentMap.FindAction("TelepathyMode");

            moveAction.performed += onMove;
            lookAction.performed += onLook;
            runAction.performed += onRun;
            telepathyMode.performed += onTelepathyMode;

            moveAction.canceled += onMove;
            lookAction.canceled += onLook;
            runAction.canceled += onRun;
            telepathyMode.canceled -= onTelepathyMode;
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
            TelepathyMode = true;  // Set to true when pressed
            telepathyPressedThisFrame = true; // Track if pressed this frame
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
                telepathyPressedThisFrame = false; // Reset for the next frame
                return true;
            }
            return false;
        }
    }
}
