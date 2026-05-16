using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//solo muestra un cartel para las ocaciones en las que no se puede usar la puerta porque es de noche
public class TerminarDIa : MonoBehaviour
{
    [SerializeField] private GameObject cartelUI;

    void Start()
    {
        cartelUI.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (cartelUI != null)
        {
            cartelUI.SetActive(true);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (cartelUI != null)
        {
            cartelUI.SetActive(false);
        }
    }
}
