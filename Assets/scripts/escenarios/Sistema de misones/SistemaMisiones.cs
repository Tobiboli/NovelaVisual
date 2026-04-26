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
    public TMP_Text textoPuntaje;

    [Header("Datos de la Misión")]
    public int misionID;
    public int puntosDeConfianza = 50;
    [TextArea] public string descripcion;
    [TextArea] public string mensajeCompletado;

    private bool enRango = false;
    public static int puntajeTotal = 0;
    public static bool[] misionesCompletadas = new bool[100];
    public static bool[] misionesIniciadas = new bool[100];

    void Start()
    {
        ActualizarInterfazPuntaje();
        if (panelMision != null) panelMision.SetActive(false);
    }

    void Update()
    {
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            Interactuar();
        }
    }

    public void Interactuar()
    {
        if (misionesCompletadas[misionID])
        {
            MostrarMensajeVictoria();
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

    public void MostrarMensajeVictoria()
    {
        panelMision.SetActive(true);
        textoMision.text = mensajeCompletado + "\n Confianza: +" + puntosDeConfianza;
        ActualizarInterfazPuntaje();
    }

    public void ActualizarInterfazPuntaje()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntaje: " + puntajeTotal;
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
            panelMision.SetActive(false);
        }
    }
}