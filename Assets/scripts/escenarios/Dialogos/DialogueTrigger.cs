using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueManager dialogueManager;

    public DialogueNode firstNode;

    public bool startAutomatically = true;

    private void Start()
    {
        if (startAutomatically)
        {
            TriggerDialogue();
        }
    }

    // Método público para iniciar el diálogo cunado se inicie la escena (si se pudo profa :D)
    public void TriggerDialogue()
    {
        if (dialogueManager != null && firstNode != null)
        {
            dialogueManager.StartDialogue(firstNode);
        }
        else
        {
            Debug.LogWarning("Falta asignar el DialogueManager o el FirstNode en el inspector.");
        }
    }
}
