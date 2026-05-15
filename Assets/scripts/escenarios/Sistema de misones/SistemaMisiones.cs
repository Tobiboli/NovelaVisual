using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SistemaMisiones : MonoBehaviour
{
    [Header("Configuración de UI")]
    public GameObject panelMision;
    public TMP_Text textoMision;

    [Header("Datos de la Misión")]
    public int misionID;
    public int puntosDeConfianza = 50;
    [TextArea] public string descripcion;
    [TextArea] public string mensajeCompletado;

    [Header("Sistema de Rivalidad")]
    public int rivalID = -1; 
    public int penalizacionRival = 20; 

    private bool enRango = false;
    public static int puntajeTotal = 0;
    public static bool[] misionesCompletadas = new bool[100];
    public static bool[] misionesIniciadas = new bool[100];
    public static bool[] recompensaEntregada = new bool[100];

 
    //si se incia dialogo aparece el panel de texto
    void Start()
    {
        if (panelMision != null) panelMision.SetActive(false);
    }
    //el jugador debe presionar E para interactuar
    void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }
    }
    //logica de interaccion por si el jugador icia misiion, no ha terminado su misión o ha terminado la mision
    public void Interactuar()
    {
        if (misionesCompletadas[misionID])
        {
            if (!recompensaEntregada[misionID])
            {
                EntregarRecompensa();
            }
            else
            {
                panelMision.SetActive(true);
                textoMision.text = "Gracias por tu ayuda anterior. No tengo más misiones por ahora.";
            }
        }
        else if (!misionesIniciadas[misionID])
        {
            misionesIniciadas[misionID] = true;
            panelMision.SetActive(true);
            textoMision.text = "Misión Iniciada: " + descripcion;
        }
        else
        {
            panelMision.SetActive(true);
            textoMision.text = "Aún no terminas: " + descripcion;
        }
    }

    //Sistema para deetectar que la recompesa fue entregada y que no sigua sumando puntos, es algo asi como un candado
    public void EntregarRecompensa()
    {
        recompensaEntregada[misionID] = true; 

        panelMision.SetActive(true);

        if (RelacionesManager.instancia != null)
        {
            RelacionesManager.instancia.ProcesarDecision(misionID, puntosDeConfianza, rivalID, penalizacionRival);
        }

        textoMision.text = mensajeCompletado + "\n Confianza: +" + puntosDeConfianza;
    }
    //cuando terminas la mision hay un mensaje por parte del NPC y se aplica la penalización 
    public void MostrarMensajeVictoria()
    {
        panelMision.SetActive(true);
        RelacionesManager.instancia.ProcesarDecision(misionID, puntosDeConfianza, rivalID, penalizacionRival);

        textoMision.text = mensajeCompletado + "\n Confianza: +" + puntosDeConfianza;
    }

    //detectar la colision de los persoanjes en pantalla
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) enRango = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enRango = false;
            panelMision.SetActive(false);
        }
    }
}