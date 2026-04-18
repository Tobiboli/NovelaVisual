using UnityEngine;

public class cafetera : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Renderer>().material.color = Color.cyan;
    }
}
