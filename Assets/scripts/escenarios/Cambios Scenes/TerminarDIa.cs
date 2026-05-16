using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminarDIa : MonoBehaviour
{
    [SerializeField] private GameObject cartelUI;

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
