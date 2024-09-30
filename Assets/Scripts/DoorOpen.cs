using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private InputAction rotateAction;
    [SerializeField] private float rotationSpeed = 90f; // Speed of rotation
    [SerializeField] private float rotationDuration = 1f; // Duration of rotation in seconds
    [SerializeField] private float maxDistance = 5f; // Maximum distance for rotation to occur
    [SerializeField] private float lineOfSightAngle = 45f; // Angle in degrees for line of sight check

    [SerializeField] private int keysLimit;
    [SerializeField] private bool winDoor;

    [SerializeField] private GameObject panel;
    private Inv inv;

    private bool isRotating = false;
    private bool rotateClockwise = true; // Flag to determine rotation direction
    private Transform playerTransform; // Reference to the player's transform

    private Coroutine rotateCoroutine;
    private Vector3 playerPosition;

    private void Awake()
    {
        rotateAction = new InputAction("Rotate", binding: "<Keyboard>/e");
        rotateAction.Enable();
        rotateAction.performed += RotateObject;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        inv = FindObjectOfType<Inv>();

        panel.SetActive(false);
    }

    private void RotateObject(InputAction.CallbackContext context)
    {
        if (!isRotating && CanRotate())
        {
            rotateCoroutine = StartCoroutine(RotateCoroutine());
        }
    }

    private bool CanRotate()
    {
        if (playerTransform == null || inv.keys < keysLimit)
            return false;

        playerPosition = playerTransform.position;

        return IsWithinRange() && IsLookingAtDoor();
    }

    private bool IsWithinRange()
    {
        float distance = Vector3.Distance(transform.position, playerPosition);
        return distance <= maxDistance;
    }

    private bool IsLookingAtDoor()
    {
        Vector3 directionToDoor = transform.position - playerPosition;
        float angle = Vector3.Angle(playerTransform.forward, directionToDoor);

        return angle <= lineOfSightAngle;
    }

    private IEnumerator RotateCoroutine()
    {
        isRotating = true;

        Quaternion initialRotation = transform.rotation;
        float targetRotationY = rotateClockwise ? initialRotation.eulerAngles.y + rotationSpeed : initialRotation.eulerAngles.y - rotationSpeed;
        Quaternion targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, targetRotationY, initialRotation.eulerAngles.z);

        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
        rotateClockwise = !rotateClockwise;

        inv.keys -= keysLimit;

        if (winDoor)
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnDisable()
    {
        rotateAction.performed -= RotateObject;
        rotateAction.Disable();

        if (rotateCoroutine != null)
        {
            StopCoroutine(rotateCoroutine);
        }
    }
}
