using UnityEngine;
using System.Collections;
using UnityEngine.PostProcessing;
using UnityEngine.Rendering.PostProcessing;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float percentComplete;
    public float shakeRange = 0.5f;
    public IEnumerator Shake(float duration, float magnitude, PostProcessProfile targetProfile, PostProcessVolume volume)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
             percentComplete = elapsed / duration;

            float curveValue = Mathf.Sin(percentComplete * Mathf.PI); // Range from 0 to 1

            float x = Random.Range(-shakeRange, shakeRange) * magnitude * curveValue;
            float y = Random.Range(-shakeRange, shakeRange) * magnitude * curveValue;

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            if (percentComplete >= 0.5)
                volume.profile = targetProfile;

                yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
