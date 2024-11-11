using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityTutorial.Manager;

public class NoteController : MonoBehaviour
{   
    private GameObject noteOpenText;
    private TextMeshProUGUI noteTMP;
    private InputManager inputManager;

    [SerializeField] GameObject notePanel;
    [SerializeField] string noteText;  
    [SerializeField] bool isReachable;
    [SerializeField] bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        noteTMP = notePanel.GetComponentInChildren<TextMeshProUGUI>();
        noteOpenText = FindFirstObjectByType<IDN_NoteText>(FindObjectsInactive.Include).gameObject;
        inputManager = FindFirstObjectByType<InputManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach") && !isOpen)
        {
            ToggleCommandText(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            ToggleCommandText(false);
        }
    }
    void Update()
    {
        if (inputManager.IsClosePressedThisFrame()) CloseNote();
        if (isReachable && inputManager.IsInteractionPressedThisFrame())
        {
            if (isOpen)
            {
               return;
            }
            else
                OpenNote();
        }
    }

    private void OpenNote()
    {
        isOpen = true;
        notePanel.SetActive(true);
        noteTMP.text = noteText;
        ToggleCommandText(false );
    }
    public void CloseNote()
    {
        isOpen = false;
        notePanel.SetActive(false);
    }

    private void ToggleCommandText(bool toggle)
    {
        isReachable = toggle;
        noteOpenText.SetActive(toggle);
    }
}
