using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;

    public int vidaMaxima = 100;
    private int vidaActual;
    public int danioAtaque = 25;
    public float rangoAtaque = 2f;
    private CharacterController controlador;

    void Start()
    {
        vidaActual = vidaMaxima;
        controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movimiento = new Vector3(horizontal, 0f, vertical).normalized;

        controlador.Move(movimiento * velocidad * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atacar();
        }
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Max(vidaActual, 0);

        Debug.Log("Vida del jugador: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("El jugador murió");
        Destroy(gameObject);
    }

    void Atacar()
    {
        Debug.Log("El jugador atacó. Daño: " + danioAtaque);

        Collider[] enemigosCercanos = Physics.OverlapSphere(transform.position, rangoAtaque);

        foreach (Collider collider in enemigosCercanos)
        {
            EnemyController enemigo = collider.GetComponent<EnemyController>();

            if (enemigo != null)
            {
                enemigo.RecibirDanio(danioAtaque);
            }
        }
    }
}