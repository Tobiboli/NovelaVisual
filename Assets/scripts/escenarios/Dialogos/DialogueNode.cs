using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Estructura de nuestros nodos
[System.Serializable]
public struct DialogueLine
{
    public string characterName;
    [TextArea(3, 5)] public string text;
    public Sprite characterSprite;
}

[System.Serializable]
public struct DialogueChoice
{
    public string choiceText;
    //Conexion entre nodos que permite la ramificacion de rutas y opciones en el juego
    public DialogueNode nextNode;
    public int numerodelasiguietescena2;
}

//el nodo con el que se va atrabajr el dialgoo, contiene las lineas de dialogo, las opciones de dialogo y la conexion entre nodos
[CreateAssetMenu(fileName = "NuevoNodoDialogo", menuName = "Sistema de Diálogos/Nodo de Diálogo")]
public class DialogueNode : ScriptableObject
{
    public List<DialogueLine> dialogueLines;

    public List<DialogueChoice> choices;


    public DialogueNode nextNode;

    public bool IsChoiceNode => choices != null && choices.Count > 0;
}
