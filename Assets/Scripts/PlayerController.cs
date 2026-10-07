using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;

    public int vidaMaxima = 100;
    private int vidaActual;
    public int danioAtaque = 25;
    public float rangoAtaque = 2f;
    private CharacterController controlador;
    public Transform puntoReaparicion;
    public float tiempoEntreDanios = 1f;
    private float proximoDanio = 0f;

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
        Debug.Log("El jugador murió y reaparece en la base");

        vidaActual = vidaMaxima;

        controlador.enabled = false;
        transform.position = puntoReaparicion.position;
        controlador.enabled = true;

        Debug.Log("Jugador reapareció con vida: " + vidaActual);
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        EnemyController enemigo = hit.gameObject.GetComponent<EnemyController>();

        if (enemigo != null && Time.time >= proximoDanio)
        {
            RecibirDanio(enemigo.danio);
            proximoDanio = Time.time + tiempoEntreDanios;
        }
    }
}