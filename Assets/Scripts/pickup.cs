using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class pickup : MonoBehaviour
{
    InputAction pick;
    Transform playerTransform;
    float maxDistance = 2;

    Inv inv;
    private void Awake()
    {
        pick = new InputAction("Pick", binding: "<Keyboard>/f");
        pick.Enable();
        pick.performed += Pick;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        inv = FindObjectOfType<Inv>();
    }


    void Pick(InputAction.CallbackContext context)
    {
        if (IsWithinRange()) { inv.hasLighter = true; gameObject.SetActive(false); }
    }
    private bool IsWithinRange()
    {
        if (playerTransform == null)
            return false;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        return distance <= maxDistance;
    }
}