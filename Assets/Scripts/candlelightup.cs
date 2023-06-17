using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class candlelightup : MonoBehaviour
{
    [SerializeField] private Light light;
    [SerializeField] private GameObject key;

    private InputAction pick;
    private Inv inv;
    private Transform playerTransform;
    private float maxDistance = 2f;

    private void Awake()
    {
        pick = new InputAction("Light", binding: "<Keyboard>/f");
        pick.Enable();
        pick.performed += Pick;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        inv = FindObjectOfType<Inv>();

        light.enabled = false;
        key.SetActive(false);
    }

    private void Pick(InputAction.CallbackContext context)
    {
        if (CanLightUp())
        {
            light.enabled = true;
            key.SetActive(true);
        }
    }

    private bool CanLightUp()
    {
        if (playerTransform == null || !inv.hasLighter)
            return false;

        return IsWithinRange();
    }

    private bool IsWithinRange()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        return distance <= maxDistance;
    }
}
