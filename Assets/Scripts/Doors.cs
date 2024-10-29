using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityTutorial.Manager;

public class Doors : MonoBehaviour
{
    private Animator doorAnimator;
    private GameObject doorTextGameObject;
    private TextMeshProUGUI doorTMP;
    private AudioSource doorSound;
    private InputManager inputManager;

    [SerializeField] bool isReachable;
    [SerializeField] bool isOpen = false;
    void Start()
    {   
        doorTextGameObject = FindFirstObjectByType<DoorText>(FindObjectsInactive.Include).gameObject;
        doorTMP = doorTextGameObject.GetComponent<TextMeshProUGUI>();
        doorSound = FindFirstObjectByType<DoorOpenAudio>().gameObject.GetComponent<AudioSource>();
        doorAnimator = GetComponent<Animator>();
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