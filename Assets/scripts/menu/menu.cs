using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void jugar()
    {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }  
    public void finales()
    {
        Debug.Log("Sin finales...");
        Application.Quit();
    }
    }
