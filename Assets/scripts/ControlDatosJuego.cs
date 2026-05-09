using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuadado;
    public DatosJuego datosJuego = new DatosJuego();

    private void Awake()
    {
        archivoDeGuadado = Application.dataPath + "/datosJuego.json";

        jugador = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }
    private void CargarDatos()
    {
        if (File.Exists(archivoDeGuadado))
        {
            string contenido = File.ReadAllText(archivoDeGuadado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            Debug.Log("Posicion Jugador: " + datosJuego.posicion);

            jugador.transform.position = datosJuego.posicion;
        }
        else
        {
            Debug.Log("El archivo no existe");
        }
    }
    private void GuardarDatos()
    {
        DatosJuego nuevosDatos = new DatosJuego()
        {
            posicion = jugador.transform.position
        };
        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoDeGuadado, cadenaJSON);

        Debug.Log("Archivo Guardado");
    }
}
