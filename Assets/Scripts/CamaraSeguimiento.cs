
using UnityEngine;

public class CamaraSeguimiento : MonoBehaviour
{
    public Transform jugador;
    public Vector3 desplazamiento = new Vector3(0f, 2f, -10f);
    public float velocidadSeguimiento = 5f;

    void LateUpdate()
    {
        if (jugador == null)
            return;

        Vector3 posicionObjetivo = jugador.position + desplazamiento;

        transform.position = Vector3.Lerp(
            transform.position,
            posicionObjetivo,
            velocidadSeguimiento * Time.deltaTime
        );
    }
}
