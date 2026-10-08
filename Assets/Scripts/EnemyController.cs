using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int vidaMaxima = 50;
    private int vidaActual;
    public int danio = 25;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Max(vidaActual, 0);

        Debug.Log("Vida del enemigo: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerController jugador = collision.gameObject.GetComponent<PlayerController>();

        if (jugador != null)
        {
            jugador.RecibirDanio(danio);
        }
    }

    void Morir()
    {
        Debug.Log("El enemigo murió");
        Destroy(gameObject);
    }
}