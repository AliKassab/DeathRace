using UnityEngine;

public class StrugglingFlickerWithFixedAudio : MonoBehaviour
{
    [SerializeField] Light flickeringLight; // Assign your spotlight here
    [SerializeField] AudioSource audioSource; // Assign your audio source here
    [SerializeField] float minIntensity = 0f; // Minimum light intensity
    [SerializeField] float maxIntensity = 1.5f; // Maximum light intensity
    [SerializeField] float flickerDuration = 0.1f; // Duration for light flickering on
    [SerializeField] float struggleDuration = 0.2f; // Duration when the light struggles to turn on
    [SerializeField] float offDuration = 0.2f; // Duration when the light is off
    [SerializeField] float fixedVolume = 1f; // Fixed volume for the buzzing sound

    private void Start()
    {
        // Set the fixed volume
        if (audioSource != null)
            audioSource.volume = fixedVolume;

        StartCoroutine(Flicker());
    }

    private System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            // Simulate struggling to turn on
            float struggleTime = Random.Range(0.05f, struggleDuration);
            for (float t = 0; t < struggleTime; t += Time.deltaTime)
            {
                // Gradually increase intensity to simulate struggle
                float currentIntensity = Mathf.Lerp(minIntensity, maxIntensity, t / struggleTime);
                flickeringLight.intensity = currentIntensity;

                // Start the audio if it's not playing
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                yield return null;
            }

            // Randomly set the intensity
            flickeringLight.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerDuration);

            // Light off for a brief moment, stop the audio
            flickeringLight.intensity = 0;

            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop(); // Stop the audio when the light is completely off
            }

            yield return new WaitForSeconds(offDuration);
        }
    }
}
