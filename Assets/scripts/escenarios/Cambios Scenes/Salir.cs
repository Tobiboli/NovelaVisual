using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Salir : MonoBehaviour
{
    [SerializeField] private GameObject cartelUI;
    [SerializeField] private int NumeroEscena;


    void Start()
    {
        cartelUI.SetActive(true);
        if (cartelUI != null)
        {
            cartelUI.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            CambiarDeEscena();
        }
    }

    public void CambiarDeEscena()
    {
        SceneManager.LoadScene(NumeroEscena);

    }

}
