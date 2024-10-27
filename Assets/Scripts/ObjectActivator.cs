using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    private void Awake()
    {
        playerController.enabled = true;
    }
}
