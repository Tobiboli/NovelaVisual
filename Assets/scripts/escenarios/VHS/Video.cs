using System.Collections;
using UnityEngine;
using TMPro;

public class Video : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject TV;


    public DialogueNode currentNode;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;
    private WaitForSeconds typingDelay;

    void Start()
    {
        if (TV != null)
        {
            TV.SetActive(false);
        }
    }

    public void StartDialogue(DialogueNode startNode)
    {
        currentNode = startNode;
        currentLineIndex = 0;

        if (TV != null)
        {
            TV.SetActive(true);
        }

        DisplayNode();
    }

    private void DisplayNode()
    {
        if (currentNode != null && currentNode.dialogueLines.Count > 0)
        {
            DisplayLine();
        }
        else
        {
            CheckNodeEnd();
        }
    }

    private void DisplayLine()
    {
        DialogueLine line = currentNode.dialogueLines[currentLineIndex];
        nameText.text = line.characterName;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeLine(line.text));
    }

    IEnumerator TypeLine(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return typingDelay;
        }

        isTyping = false;
    }

    public void AdvanceDialogue()
    {
        if (isTyping)
        {
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            dialogueText.text = currentNode.dialogueLines[currentLineIndex].text;
            isTyping = false;
            return;
        }

        currentLineIndex++;

        if (currentLineIndex < currentNode.dialogueLines.Count)
        {
            DisplayLine();
        }
        else
        {
            CheckNodeEnd();
        }
    }

    private void CheckNodeEnd()
    {

        if (currentNode.nextNode != null)
        {
            currentNode = currentNode.nextNode;
            currentLineIndex = 0;
            DisplayNode();
        }
        else
        {
            EndDialogue();

        }
    }


    private void EndDialogue()
    {
        nameText.text = "";
        dialogueText.text = "...";

        if (TV != null)
        {
            Destroy(TV);
        }
    }
}
