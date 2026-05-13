using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Choice
{
    public string buttonText;      
    public DialogueScript nextDialogue; 
}

[System.Serializable]
public struct Line
{
    public string speaker;
    [TextArea(3, 5)]
    public string text;

    [Header("Decisiones (Opcional)")]
    public List<Choice> choices; 
}

[CreateAssetMenu(fileName = "NewConversation", menuName = "Dialogues/Conversation")]
public class DialogueScript : ScriptableObject
{
    public List<Line> conversationLines;
}