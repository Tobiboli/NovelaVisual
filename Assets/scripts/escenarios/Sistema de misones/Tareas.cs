using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tareas : MonoBehaviour
{
    [Header("Configuración de la Tarea")]
    public int misionID;
    public float tiempoRequerido = 5f;
    private float tiempoActual = 0f;

    [Header("Interfaz de Carga")]
    public GameObject panelProgreso;
    public Slider barraProgreso;
    public TMP_Text textoEstado;

    private bool enRango = false;
    private bool tareaTerminada = false;
    //aparece una barra que marca el progreso de tu tarea y el jugador debe esperar 5 segundos con el boton E presionado
    void Start()
    {
        if (panelProgreso != null) panelProgreso.SetActive(false);
        if (barraProgreso != null) barraProgreso.maxValue = tiempoRequerido;
    }
    //identifica si el jugador no ha presionado la tecla E 5 segundos y reinicia el progreso
    void Update()
    {
        if (enRango && SistemaMisiones.misionesIniciadas[misionID] && !tareaTerminada)
        {
            if (Input.GetKey(KeyCode.E))
            {
                CargandoTarea();
            }
            else
            {
                ResetearProgreso();
            }
        }
    }
    //logica de la barra de progreso
    void CargandoTarea()
    {
        if (panelProgreso != null) panelProgreso.SetActive(true);

        tiempoActual += Time.deltaTime;
        if (barraProgreso != null) barraProgreso.value = tiempoActual;

        if (textoEstado != null)
            textoEstado.text = "Progreso: " + (int)((tiempoActual / tiempoRequerido) * 100) + "%";

        if (tiempoActual >= tiempoRequerido)
        {
            FinalizarTarea();
        }
    }
    //logica del resteo del progreso
    void ResetearProgreso()
    {
        tiempoActual = 0f;
        if (barraProgreso != null) barraProgreso.value = 0f;
        if (panelProgreso != null && panelProgreso.activeSelf)
            panelProgreso.SetActive(false);
    }
    //Cuando la tarea se completa los datos se actualiza, las barras de progreso aumentan o se aplican las penalizacione gracias al ID que se les asigna
    void FinalizarTarea()
    {
        tareaTerminada = true;
        if (panelProgreso != null) panelProgreso.SetActive(false);

        SistemaMisiones npcDueño = null;
        SistemaMisiones[] todosLosNPCs = FindObjectsOfType<SistemaMisiones>();

        foreach (SistemaMisiones npc in todosLosNPCs)
        {
            if (npc.misionID == this.misionID)
            {
                npcDueño = npc;
                break;
            }
        }

        if (RelacionesManager.instancia != null && npcDueño != null)
        {
            RelacionesManager.instancia.ProcesarDecision(
                misionID,
                100,
                npcDueño.rivalID,
                npcDueño.penalizacionRival
            );

        }

        SistemaMisiones.misionesCompletadas[misionID] = true;

        Debug.Log("¡Tarea " + misionID + " finalizada con éxito!");

        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
            sr.color = Color.blue;
    }
    //detecta colision entre objetos 
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) enRango = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enRango = false;
            ResetearProgreso();
        }
    }
}