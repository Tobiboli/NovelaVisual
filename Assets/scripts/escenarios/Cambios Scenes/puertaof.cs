using UnityEngine;
using UnityEngine.SceneManagement;

public class puertaof : MonoBehaviour
{

    [SerializeField] private GameObject cartelUI;
    [SerializeField] private int npcRutaA_ID;
    [SerializeField] private int npcRutaB_ID;
    [SerializeField] private int numerodelasiguietescenaA;
    [SerializeField] private int numerodelasiguietescenaB;
    [SerializeField] private int numerodelasiguietescenaFinal;




    void Start()
    {
        if (cartelUI != null)
        {
            cartelUI.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MostrarCartel();
        }
    }

    void MostrarCartel()
    {
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
        if (RelacionesManager.instancia != null)
        {
            int puntosNPC_A = RelacionesManager.instancia.GetConfianza(npcRutaA_ID);
            int puntosNPC_B = RelacionesManager.instancia.GetConfianza(npcRutaB_ID);

            if (puntosNPC_A > puntosNPC_B)
            {
                SceneManager.LoadScene(numerodelasiguietescenaA);
            }
            else if (puntosNPC_B > puntosNPC_A)
            {
                SceneManager.LoadScene(numerodelasiguietescenaB);
            }
            else
            {
                // Final malo por flojo :p
                SceneManager.LoadScene(numerodelasiguietescenaFinal);
            }
        }
    }

    public void CerrarCartel()
    {
        if (cartelUI != null)
        {
            cartelUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

