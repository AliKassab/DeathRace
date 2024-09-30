using UnityEngine;

public class HeadMountedLight : MonoBehaviour
{
    public Transform playerHead;  // Reference to the player's head (or camera)
    public Light headLight;       // Reference to the light component
    public float lightSwayAmount = 0.1f;  // The amount of sway during movement
    public float swaySpeed = 3.0f;        // Speed of the sway (the faster, the quicker the bounce)
    public float lightFollowSpeed = 5.0f; // Speed of light following head movement

    private Vector3 initialLightPosition; // Initial local position of the light
    private float swayOffset;             // Offset for sway calculation

    void Start()
    {
        if (!playerHead)
        {
            Debug.LogError("Player head reference is missing!");
        }

        if (!headLight)
        {
            Debug.LogError("Head light reference is missing!");
        }

        // Store the initial position of the light (relative to the player head)
        initialLightPosition = headLight.transform.localPosition;
    }

    void Update()
    {
        FollowHeadMovement();
        ApplyLightSway();
    }

    // Function to make the light follow the player's head movement smoothly
    void FollowHeadMovement()
    {
        // Interpolate light position towards the player's head position
        Vector3 targetPosition = playerHead.position + initialLightPosition;
        headLight.transform.position = Vector3.Lerp(headLight.transform.position, targetPosition, lightFollowSpeed * Time.deltaTime);

        // Make the light rotate to match the player's head rotation
        headLight.transform.rotation = Quaternion.Lerp(headLight.transform.rotation, playerHead.rotation, lightFollowSpeed * Time.deltaTime);
    }

    // Function to add a subtle sway effect based on player movement (e.g., walking or running)
    void ApplyLightSway()
    {
        // Calculate sway using a sinusoidal pattern for smooth bouncing
        swayOffset += Time.deltaTime * swaySpeed;
        float swayAmount = Mathf.Sin(swayOffset) * lightSwayAmount;

        // Apply sway offset to the light's local position
        Vector3 swayPosition = headLight.transform.localPosition;
        swayPosition.y = initialLightPosition.y + swayAmount;
        headLight.transform.localPosition = swayPosition;
    }
}
