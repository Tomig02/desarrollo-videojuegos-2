using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IObjetivoEnemigo, IRecolector
{
    public float velocidad = 5f;
    public int vidaMaxima = 100;
    private int vidaActual;
    private float tiempoVisualizacionAtaque = 0f;

    public static int mejorasDeVida = 0;

    public static Inventario inventarioJugador = new Inventario();

    public void AgregarItem(ItemScriptableObject item)
    {
        if (item == null) return;

        inventarioJugador.AgregarItem(item, 1);
        Debug.Log("Objeto recogido: " + item.Nombre);
    }

    public int danioAtaque = 25;
    public float rangoAtaque = 2f;
    private CharacterController controlador;
    private Animator animador;
    public Transform puntoReaparicion;
    [SerializeField] private PopupEventSO popupEvent;
    private bool estaMuerto = false;

    void Start()
    {
        vidaMaxima += mejorasDeVida * 25;
        vidaActual = vidaMaxima;

        controlador = GetComponent<CharacterController>();
        animador = GetComponentInChildren<Animator>();

        Debug.Log("Vida máxima del jugador: " + vidaMaxima);
    }

    void Update()
    {
        if (estaMuerto) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movimiento = new Vector3(horizontal, 0f, vertical).normalized;

        controlador.Move(movimiento * velocidad * Time.deltaTime);
        animador.SetFloat("Speed", movimiento.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atacar();
        }

        tiempoVisualizacionAtaque -= Time.deltaTime;
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
        if (estaMuerto) return;

        estaMuerto = true;

        Debug.Log("El jugador murió. Mostrando pop-up.");

        if (popupEvent != null)
        {
            popupEvent.Raise(
                "¿Reintentar?",
                "Tu personaje ha muerto. ¿Querés volver a intentarlo?",
                new RestartLevelCommand(),
                new ExitGameCommand()
            );

            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("No se asignó el evento del pop-up.");
        }
    }


    void Atacar()
    {
        tiempoVisualizacionAtaque = 0.3f;

        Debug.Log("El jugador atacó. Daño: " + danioAtaque);

        Collider[] enemigosCercanos = Physics.OverlapSphere(transform.position, rangoAtaque);

        HashSet<ComportamientoEnemigo> enemigosGolpeados = new HashSet<ComportamientoEnemigo>();

        foreach (Collider collider in enemigosCercanos)
        {
            ComportamientoEnemigo enemigo =
                collider.GetComponentInParent<ComportamientoEnemigo>();

            if (enemigo != null && enemigosGolpeados.Add(enemigo))
            {
                enemigo.RecibirDanio(danioAtaque);
            }
        }
    }



    public Vector3 PosicionActual()
    {
        return transform.position;
    }

    void OnDrawGizmos()
    {
        // Rango de ataque
        Gizmos.color = tiempoVisualizacionAtaque > 0
            ? Color.red
            : Color.gray;

        Gizmos.DrawWireSphere(transform.position, rangoAtaque);

        // Punto de reaparición
        if (puntoReaparicion != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(puntoReaparicion.position, 0.3f);
            Gizmos.DrawLine(transform.position, puntoReaparicion.position);
        }
    }
}