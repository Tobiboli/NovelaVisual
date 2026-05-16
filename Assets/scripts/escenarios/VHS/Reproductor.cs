using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Reproductor : MonoBehaviour
{
    public Video Video;
    public DialogueNode firstNode;


    public int NumeroVHS;

    private bool enRango = false;
    private bool Visto = false;


    void Update()
    {
        if (enRango && VHS.VHSpendeinte[NumeroVHS] && !Visto)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
            }
        }
    }

    public void TriggerDialogue()
    {
        if (Video != null && firstNode != null)
        {
            Video.StartDialogue(firstNode);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) enRango = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enRango = false;
        }
    }
}