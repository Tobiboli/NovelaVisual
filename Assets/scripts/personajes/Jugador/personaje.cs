using UnityEngine;

// Este script mueve un jugador en 2D usando Rigidbody2D
public class personaje : MonoBehaviour
{
    public float velocidad = 5f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        float moverX = Input.GetAxisRaw("Horizontal");
        float moverY = Input.GetAxisRaw("Vertical");

        Vector2 movimiento = new Vector2(moverX, moverY);

        transform.Translate(movimiento * velocidad * Time.deltaTime);

        animator.SetFloat("Horizontal", moverX);
        animator.SetFloat("Vertical", moverY);
        animator.SetFloat("Speed", movimiento.magnitude);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;
    }
}