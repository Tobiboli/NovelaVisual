using UnityEngine;
using UnityEngine.SceneManagement;
public class puertacuar : MonoBehaviour
{
    public int NumeroVHS;
    [SerializeField] private int NumeroEscena;
    [SerializeField] private GameObject cartelUI;


    void Start()
    {
        if (cartelUI != null)
        {
            cartelUI.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (VHS.VHSpendeinte[NumeroVHS]))
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
        SceneManager.LoadScene(NumeroEscena);
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
