using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI General")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI lineText;
    [SerializeField] private GameObject dialoguePanel;

    [Header("Panel de Decisiones")]
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button buttonA, buttonB, buttonC;
    [SerializeField] private TextMeshProUGUI textA, textB, textC;

    private bool dialogueActive;
    private bool awaitingChoice;
    private Queue<Line> lines = new Queue<Line>();

    void Start()
    {
        if (choicePanel != null) choicePanel.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (dialogueActive && !awaitingChoice && Input.GetKeyDown(KeyCode.Space))
        {
            ProduceNextLine();
        }
    }

    public void GetConversation(DialogueScript dialogue)
    {
        if (dialogueActive) return;

        StartDialogueSequence(dialogue);
    }

    private void StartDialogueSequence(DialogueScript dialogue)
    {
        lines.Clear();
        foreach (var line in dialogue.conversationLines) lines.Enqueue(line);

        dialogueActive = true;
        awaitingChoice = false;
        dialoguePanel.SetActive(true);
        choicePanel.SetActive(false);

        ProduceNextLine();
    }

    void ProduceNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        Line currentLine = lines.Dequeue();
        nameText.text = currentLine.speaker;
        lineText.text = currentLine.text;

        if (currentLine.choices != null && currentLine.choices.Count > 0)
        {
            SetupChoices(currentLine.choices);
        }
        else
        {
            choicePanel.SetActive(false);
            awaitingChoice = false;
        }
    }

    void SetupChoices(List<Choice> choices)
    {
        awaitingChoice = true;
        choicePanel.SetActive(true);

        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();
        buttonC.onClick.RemoveAllListeners();

        if (choices.Count > 0)
        {
            textA.text = choices[0].buttonText;
            buttonA.onClick.AddListener(() => OnChoiceSelected(choices[0].nextDialogue));
        }

        if (choices.Count > 1)
        {
            textB.text = choices[1].buttonText;
            buttonB.onClick.AddListener(() => OnChoiceSelected(choices[1].nextDialogue));
        }

        if (choices.Count > 2)
        {
            textC.text = choices[2].buttonText;
            buttonC.onClick.AddListener(() => OnChoiceSelected(choices[2].nextDialogue));
        }
    }

    void OnChoiceSelected(DialogueScript nextDialogue)
    {

        awaitingChoice = false;
        choicePanel.SetActive(false);

        if (nextDialogue != null)
        {
            StartDialogueSequence(nextDialogue);
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueActive = false;
        awaitingChoice = false;
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        Debug.Log("Diálogo terminado.");
    }
}