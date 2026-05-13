using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueScript myDialogue; 

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().GetConversation(myDialogue);
    }
}