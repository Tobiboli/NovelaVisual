using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//controlador de los nodos en pantalla
public class DialogueManager : MonoBehaviour
{
    [Header("Componentes de UI")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;

    [Header("Configuración de Opciones")]
    public GameObject choiceButtonPrefab;
    public Transform choiceButtonsContainer;

    [Header("Ajustes")]
    public float typingSpeed = 0.02f;

    private DialogueNode currentNode;
    private int currentLineIndex = 0;
    private bool isTyping = false;

    public void StartDialogue(DialogueNode startNode)
    //La lista al leer los dialogos
    {
        currentNode = startNode;
        currentLineIndex = 0;
        DisplayNode();
    }

    private void DisplayNode()
    {
        ClearChoices();

        if (currentNode.dialogueLines.Count > 0)
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
        //Controlador de las imagenes del personaje 
        if (characterImage != null)
        {
            if (line.characterSprite != null)
            {
                characterImage.sprite = line.characterSprite;
                characterImage.gameObject.SetActive(true);
            }
            else
            {
                characterImage.gameObject.SetActive(false);
            }
        }

        StopAllCoroutines();
        StartCoroutine(TypeLine(line.text));
    }

    IEnumerator TypeLine(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        //si hay opciones el sistema manda a las opciones
        if (currentLineIndex == currentNode.dialogueLines.Count - 1 && currentNode.IsChoiceNode)
        {
            ShowChoices();
        }
    }
    //logica del codigo, determina si hay dialogo lineal, opciones o se termino el dialogo
    public void AdvanceDialogue()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentNode.dialogueLines[currentLineIndex].text;
            isTyping = false;

            if (currentLineIndex == currentNode.dialogueLines.Count - 1 && currentNode.IsChoiceNode)
            {
                ShowChoices();
            }
            return;
        }

        if (currentLineIndex == currentNode.dialogueLines.Count - 1 && currentNode.IsChoiceNode)
        {
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
        if (currentNode.IsChoiceNode)
        {
            ShowChoices();
        }
        else if (currentNode.nextNode != null)
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

    private void ShowChoices()
    {
        ClearChoices();

        foreach (DialogueChoice choice in currentNode.choices)
        {
            GameObject buttonObj = Instantiate(choiceButtonPrefab, choiceButtonsContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => OnChoiceSelected(choice.nextNode));
        }
    }

    private void OnChoiceSelected(DialogueNode nextNode)
    {
        if (nextNode != null)
        {
            currentNode = nextNode;
            currentLineIndex = 0;
            DisplayNode();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ClearChoices()
    {
        foreach (Transform child in choiceButtonsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void EndDialogue()
    {
        nameText.text = "";
        dialogueText.text = "...";
        if (characterImage != null) characterImage.gameObject.SetActive(false);
        ClearChoices();
        Debug.Log("Fin de la conversación.");
    }
}
