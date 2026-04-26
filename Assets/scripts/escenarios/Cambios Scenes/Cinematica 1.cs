
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cinematica1 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(3);
    }
}