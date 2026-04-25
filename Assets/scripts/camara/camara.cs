using UnityEngine;

public class camaraoficina : MonoBehaviour
{
    public Transform player; // El jugador a seguir
    public float minX, maxX, minY, maxY; // Límites

    void LateUpdate()
    {
        // Obtener posición actual
        Vector3 newPos = transform.position;

        // Limitar posición usando Mathf.Clamp
        newPos.x = Mathf.Clamp(player.position.x, minX, maxX);
        newPos.y = Mathf.Clamp(player.position.y, minY, maxY);

        // Aplicar posición
        transform.position = newPos;
    }
}