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

    void Start()
    {
        if (panelProgreso != null) panelProgreso.SetActive(false);
        if (barraProgreso != null) barraProgreso.maxValue = tiempoRequerido;
    }

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

    void CargandoTarea()
    {
        panelProgreso.SetActive(true);
        tiempoActual += Time.deltaTime;
        barraProgreso.value = tiempoActual;

        if (textoEstado != null)
            textoEstado.text = "Progreso: " + (int)((tiempoActual / tiempoRequerido) * 100) + "%";

        if (tiempoActual >= tiempoRequerido)
        {
            FinalizarTarea();
        }
    }

    void ResetearProgreso()
    {
        tiempoActual = 0f;
        if (barraProgreso != null) barraProgreso.value = 0f;
        if (panelProgreso != null && panelProgreso.activeSelf)
            panelProgreso.SetActive(false);
    }

    void FinalizarTarea()
    {
        tareaTerminada = true;
        if (panelProgreso != null) panelProgreso.SetActive(false);

        SistemaMisiones.misionesCompletadas[misionID] = true;
        SistemaMisiones.puntajeTotal += 100;

        SistemaMisiones[] todosLosNPCs = FindObjectsOfType<SistemaMisiones>();
        foreach (SistemaMisiones npc in todosLosNPCs)
        {
            if (npc.misionID == this.misionID)
            { }
        }

        Debug.Log("¡Tarea " + misionID + " finalizada y NPC notificado!");

        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
            sr.color = Color.blue;
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
            ResetearProgreso();
        }
    }
}