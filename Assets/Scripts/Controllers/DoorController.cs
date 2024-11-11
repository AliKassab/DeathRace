using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityTutorial.Manager;

public class DoorController : MonoBehaviour
{
    private Animator doorAnimator;
    private GameObject doorTextGameObject;
    private TextMeshProUGUI doorTMP;
    private AudioSource doorSound;
    private InputManager inputManager;

    [SerializeField] bool isReachable;
    [SerializeField] bool isOpen = false;
    void Start()
    {   inputManager = FindFirstObjectByType<InputManager>();
        doorTextGameObject = FindFirstObjectByType<IDN_DoorText>(FindObjectsInactive.Include).gameObject;
        doorSound = FindFirstObjectByType<IDN_DoorAudio>().gameObject.GetComponent<AudioSource>();
        doorTMP = doorTextGameObject.GetComponent<TextMeshProUGUI>();
        doorAnimator = GetComponent<Animator>();
        isReachable = false;      
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            isReachable = true;
            doorTextGameObject.SetActive(true);
            if (isOpen)
                doorTMP.text = "Close [F]";
            else
                doorTMP.text = "Open [F]";
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
                doorTMP.text = "Close [F]";
                CloseDoor();
            }
            else
            {
                doorTMP.text = "Open [F]";
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