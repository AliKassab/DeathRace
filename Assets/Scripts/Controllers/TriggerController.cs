using UnityEngine;
using DialogueEditor;
public class TriggerController : MonoBehaviour
{
    NPCConversation trigger;
    bool isPlayed;
   
    [SerializeField] bool HasAudio;
    [SerializeField] bool HasSpeech;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trigger = GetComponent<NPCConversation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayed)
        {
            bool isConversationInactive = !ConversationManager.Instance.IsConversationActive;
            bool isAudioNotPlaying = !ConversationManager.Instance.AudioPlayer.isPlaying;
            if ((HasSpeech && HasAudio && isConversationInactive && isAudioNotPlaying) ||
        (HasSpeech && !HasAudio && isConversationInactive) ||
        (HasAudio && !HasSpeech && isAudioNotPlaying) ||
        (!HasSpeech && !HasAudio))
            {
                Invoke(nameof(DisableTrigger), 1f);
            }
            
        }
    }

    private void DisableTrigger()
    {
        ConversationManager.Instance.EndConversation();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            ConversationManager.Instance.StartConversation(trigger);
            isPlayed = true; 
        }
    }
}
