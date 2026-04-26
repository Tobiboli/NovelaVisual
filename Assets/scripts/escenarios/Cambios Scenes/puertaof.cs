using UnityEngine;
using UnityEngine.SceneManagement;
public class puertaof : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(1);
    }
}

