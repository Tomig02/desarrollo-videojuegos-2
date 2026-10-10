using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IObjetivoEnemigo, IRecolector
{
    public float velocidad = 5f;
    public int vidaMaxima = 100;
    private int vidaActual;

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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movimiento = new Vector3(horizontal, 0f, vertical).normalized;

        controlador.Move(movimiento * velocidad * Time.deltaTime);
        animador.SetFloat("Speed", movimiento.magnitude);

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
}