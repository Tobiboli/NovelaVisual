using UnityEngine;

public class impresora : MonoBehaviour
{
   void OnCollisionEnter2D(Collision2D collision)
    {
    GetComponent<Renderer>().material.color = Color.blue;
    }
}
