using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VHS : MonoBehaviour
{
    public GameObject Aviso;
    public TMP_Text MensajePantalla;

    public int NumeroVHS;
    [TextArea] public string MensajeInicio;

    private bool enRango = false;
    public static bool[] VHSpendeinte = new bool[100];



    void Start()
    {
        if (Aviso != null) Aviso.SetActive(false);
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
        if (!VHSpendeinte[NumeroVHS])
        {
            VHSpendeinte[NumeroVHS] = true;
            Aviso.SetActive(true);
            MensajePantalla.text = "Encontraste un VHS. " + MensajeInicio;
        }
        else
        {
            Aviso.SetActive(true);
            MensajePantalla.text = "Ya tienes un VHS. " + MensajeInicio;
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
            Aviso.SetActive(false);
        }
    }
}
