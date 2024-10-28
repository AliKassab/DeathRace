using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityTutorial.Manager;

public class Doors : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] GameObject doorTextGameObject;
    TextMeshProUGUI doorTMP;
    [SerializeField] AudioSource doorSound;
    [SerializeField] bool isReachable;
    private InputManager inputManager;
    [SerializeField] bool isOpen = false;
    void Start()
    {   
        doorTextGameObject = FindFirstObjectByType<DoorText>(FindObjectsInactive.Include).gameObject;
        doorTMP = doorTextGameObject.GetComponent<TextMeshProUGUI>();
        isReachable = false;
        inputManager = FindObjectOfType<InputManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            isReachable = true;
            doorTextGameObject.SetActive(true);
            if (isOpen)
                doorTMP.text = "Close";
            else
                doorTMP.text = "Open";
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            isReachable = false;
            doorTextGameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (isReachable && inputManager.IsInteractionPressedThisFrame())
        {
            if (isOpen)
            {
                doorTMP.text = "Close";
                CloseDoor();
            }
            else
            {
                doorTMP.text = "Open";
                OpenDoor();
            }
        }
    }
    void OpenDoor()
    {
        isOpen = true;
        doorAnimator.SetBool("Open", true);
        doorSound.Play();
    }
    void CloseDoor()
    {   
        isOpen = false;
        doorAnimator.SetBool("Open", false);
    }
}