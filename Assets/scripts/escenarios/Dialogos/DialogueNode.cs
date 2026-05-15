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
}

[CreateAssetMenu(fileName = "NuevoNodoDialogo", menuName = "Sistema de Diálogos/Nodo de Diálogo")]
public class DialogueNode : ScriptableObject
{
    [Header("Conversación Lineal")]
    public List<DialogueLine> dialogueLines;

    [Header("Ramificaciones (Opciones)")]
    public List<DialogueChoice> choices;

    [Header("Siguiente Nodo Automático")]
    public DialogueNode nextNode;

    public bool IsChoiceNode => choices != null && choices.Count > 0;
}
