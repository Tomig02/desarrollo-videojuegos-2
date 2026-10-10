
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform jugador;
    private Vector3 distancia;

    void Start()
    {
        distancia = transform.position - jugador.position;
    }

    void LateUpdate()
    {
        transform.position = jugador.position + distancia;
    }
}
