using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityTutorial.Manager;

public class Doors : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;
    [SerializeField] GameObject doorText;
    TextMeshProUGUI doorTMP;
    [SerializeField] AudioSource doorSound;
    [SerializeField] bool isReachable;
    private InputManager inputManager;
    [SerializeField] bool isOpen = false;
    void Start()
    {   
        doorTMP = doorText.GetComponent<TextMeshProUGUI>();
        isReachable = false;
        inputManager = FindObjectOfType<InputManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            isReachable = true;
            doorText.SetActive(true);
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
            doorText.SetActive(false);
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