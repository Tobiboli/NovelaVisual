using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//logica del dialogo,muestra el terxto letra por letra, las opciones de dialogo y la imagen del persoanje, al finalizar cambia de escena 
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;

    public GameObject choiceButtonPrefab;
    public Transform choiceButtonsContainer;

    public float typingSpeed = 0.02f;
    [SerializeField] private int numerodelasiguietescena; 

    private DialogueNode currentNode;
    private int currentLineIndex = 0;
    private bool isTyping = false;


    private int pendienteEscena = -1;

    public void StartDialogue(DialogueNode startNode)
    {
        currentNode = startNode;
        currentLineIndex = 0;
        pendienteEscena = -1; 
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
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        isTyping = false;

        if (currentLineIndex == currentNode.dialogueLines.Count - 1 && currentNode.IsChoiceNode)
        {
            ShowChoices();
        }
    }

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

            if (pendienteEscena != -1)
            {
                SceneManager.LoadScene(pendienteEscena);
            }
            else
            {
                SceneManager.LoadScene(numerodelasiguietescena);
            }
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
            button.onClick.AddListener(() => OnChoiceSelected(choice.nextNode, choice.numerodelasiguietescena2));
        }
    }

    private void OnChoiceSelected(DialogueNode nextNode, int escenaDeEleccion)
    {

        if (escenaDeEleccion != -1)
        {
            pendienteEscena = escenaDeEleccion;
        }

        if (nextNode != null)
        {
            currentNode = nextNode;
            currentLineIndex = 0;
            DisplayNode();
        }
        else
        {
            CheckNodeEnd();
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