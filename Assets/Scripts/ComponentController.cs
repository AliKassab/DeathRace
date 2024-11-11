using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class ComponentController : MonoBehaviour
{
    [SerializeField] private Component component;
    [SerializeField] private bool state;
    private void Awake()
    {
        if (component != null)
        {
            Behaviour behaviour = component as Behaviour;
            if (behaviour != null)
            {
                behaviour.enabled = state;
            }
            else
            {
                Debug.LogWarning("Component is not of type Behaviour and cannot be enabled.");
            }
        }
        else
        {
            Debug.LogWarning("Component reference is missing.");
        }
    }
}
