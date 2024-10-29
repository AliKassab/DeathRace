using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject DialogueGameObject;
    [SerializeField] AudioClip audioClip;
    [SerializeField] string dialogue;
    [SerializeField] GameObject eventObject;
    private TextMeshProUGUI dialogueText;
    private AudioSource dialogueAudio;
    private bool isInitiated;

    private void Start()
    {
        dialogueAudio = GetComponent<AudioSource>();
        if (DialogueGameObject != null )
        dialogueText = DialogueGameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInitiated)
        {
            InitiateDialgoue();
            if (eventObject != null )
            eventObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!dialogueAudio.isPlaying && isInitiated)
        {
            gameObject.SetActive(false);
            if (DialogueGameObject != null)
            DialogueGameObject.SetActive(false);
        }
    }

    private void InitiateDialgoue()
    {   isInitiated = true;
        if (DialogueGameObject  != null)
        {
            DialogueGameObject.SetActive(true);
            dialogueText.text = dialogue;
        }   
        
        dialogueAudio.clip = audioClip;        
        dialogueAudio.Play();
    }
}
