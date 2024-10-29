using UnityEngine;

public class SimpleFlickeringLight : MonoBehaviour
{
    public Light flickerLight;         // Reference to the light component
    public float lowIntensity = 0.5f;  // Low intensity for the flicker
    public float highIntensity = 1.5f; // High intensity for the flicker
    public float flickerInterval = 0.2f; // Time between flickers (in seconds)

    private float timer;

    void Start()
    {
        if (flickerLight == null)
            flickerLight = GetComponent<Light>();

        flickerLight.intensity = highIntensity; // Start with high intensity
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= flickerInterval)
        {
            // Toggle between low and high intensity
            flickerLight.intensity = flickerLight.intensity == highIntensity ? lowIntensity : highIntensity;

            // Reset timer
            timer = 0f;
        }
    }
}
