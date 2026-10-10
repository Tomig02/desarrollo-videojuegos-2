using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public interface IObjetivoEnemigo
{
    public void RecibirDanio(int cantidad);
    public Vector3 PosicionActual();
}

public class ComportamientoEnemigo : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Collider colliderDanio;
    [SerializeField] private Collider colliderVision;
    [SerializeField] private Animator animaciones;
    [SerializeField] private NavMeshAgent agente;

    [Header("Atributos")]
    [SerializeField] private int vidaMaxima = 50;
    [SerializeField] private int danio = 25;
    [SerializeField] private bool puedeAtacar = true;
    public bool PuedeAtacar => puedeAtacar;
    [SerializeField] private float esperaLuegoDeAtaque = 2f;


    private int vidaActual;
    private List<IObjetivoEnemigo> objetivos;
    public IObjetivoEnemigo ObjetivoActual { get; private set; }
    private bool comportamientoSuspendido;

    void Start()
    {
        objetivos = new List<IObjetivoEnemigo>();
        agente.updateRotation = false;
        animaciones.SetBool("estatico", true);
        vidaActual = vidaMaxima;

        if(colliderDanio == null)
            Debug.LogError("El collider de daño no está asignado en el inspector.");
        if(colliderVision == null)
            Debug.LogError("El collider de visión no está asignado en el inspector.");
        if(animaciones == null)
            Debug.LogError("El Animator no está asignado en el inspector.");
    }

    private void Update()
    {
        if(comportamientoSuspendido)
            return;

        if (!puedeAtacar)
        {
            agente.isStopped = true;
            agente.velocity = Vector3.zero;
            agente.ResetPath();
            return;
        }

        if (objetivos.Count > 0)
        {
            ObjetivoActual = BuscarObjetivoActual();
            agente.isStopped = false;
            agente.SetDestination(ObjetivoActual.PosicionActual());
        }
        else
        {
            agente.isStopped = true;
            agente.ResetPath();
        }
    }

    public void ColisionEntrada(IObjetivoEnemigo objetivo, TriggerEnemigo tipo)
    {
        if (tipo == TriggerEnemigo.Danio)
        {
            objetivo.RecibirDanio(danio);
            puedeAtacar = false;

            agente.isStopped = true;
            agente.velocity = Vector3.zero;
            agente.ResetPath();

            StartCoroutine(EsperarYAtacar());
        }
        else if (tipo == TriggerEnemigo.Vision)
        {
            if (!objetivos.Contains(objetivo))
                objetivos.Add(objetivo);
        }

        IEnumerator EsperarYAtacar()
        {
            yield return new WaitForSeconds(esperaLuegoDeAtaque);

            puedeAtacar = true;
            agente.isStopped = false;
        }
    }
    public void ColisionSalida(IObjetivoEnemigo objetivo, TriggerEnemigo tipo)
    {
        if (tipo == TriggerEnemigo.Vision)
        {
            if (objetivos.Contains(objetivo))
                objetivos.Remove(objetivo);
        }
    }

    public void SuspenderComportamiento()
    {
        if (comportamientoSuspendido)
            return;

        comportamientoSuspendido = true;

        agente.isStopped = true;
        agente.ResetPath();
    }

    public void ReanudarComportamiento()
    {
        if (!comportamientoSuspendido)
            return;

        comportamientoSuspendido = false;

        if (puedeAtacar)
            agente.isStopped = false;
    }

    public IObjetivoEnemigo BuscarObjetivoActual()
    {
        if(objetivos.Count == 0)
            return null;

        IObjetivoEnemigo objetivoReal = null;
        float menorDistanciaSqr = float.MaxValue;

        foreach (IObjetivoEnemigo objetivo in objetivos)
        {
            Vector3 diferencia = objetivo.PosicionActual() - transform.position;
            float distanciaSqr = diferencia.sqrMagnitude;

            if (distanciaSqr < menorDistanciaSqr)
            {
                menorDistanciaSqr = distanciaSqr;
                objetivoReal = objetivo;
            }
        }

        return objetivoReal;
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
    void Morir()
    {
        Debug.Log("El enemigo murió");

        if (EconomiaBase.Instancia != null)
        {
            EconomiaBase.Instancia.AgregarMonedas(25);
        }

        Destroy(gameObject);
    }
}