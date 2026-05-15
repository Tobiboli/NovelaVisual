using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelacionesManager : MonoBehaviour
{
    public static RelacionesManager instancia;

    public int[] confianzaNPCs = new int[100];

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    //logica de puntos, hay penalizaciones si ayudas a la ruta contraria
    public void ProcesarDecision(int npcAyudadoID, int puntosGanados, int rivalID, int puntosPerdidos)
    {

        confianzaNPCs[npcAyudadoID] += puntosGanados;
        Debug.Log($"Confianza con NPC {npcAyudadoID} subió a: {confianzaNPCs[npcAyudadoID]}");

        if (rivalID != -1)
        {
            confianzaNPCs[rivalID] -= puntosPerdidos;

            if (confianzaNPCs[rivalID] < 0) confianzaNPCs[rivalID] = 0;

            Debug.Log($"Confianza con el rival (NPC {rivalID}) bajó a: {confianzaNPCs[rivalID]}");
        }
    }

    public int GetConfianza(int npcID)
    {
        return confianzaNPCs[npcID];
    }


}

