using UnityEngine;

// Este script mueve un jugador en 2D usando Rigidbody2D
public class personaje : MonoBehaviour
{
    public float velocidad = 5f;

    void Update()
    {
        float moverX = Input.GetAxis("Horizontal");
        float moverY = Input.GetAxis("Vertical");

        Vector2 movimiento = new Vector2(moverX, moverY);

        transform.Translate(movimiento * velocidad * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }
}