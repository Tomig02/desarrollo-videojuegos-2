using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ComportamientoEnemigo))]
public class EnemigoConSalto : MonoBehaviour
{
    private ComportamientoEnemigo comportamientoBase;
    [SerializeField] private float distanciaSalto = 2f;
    [SerializeField] private float duracionSalto = 0.5f;
    [SerializeField] private float alturaSalto = 1f;
    [SerializeField] private float descansoAtaque = 1f;

    private Vector3 posicionObjetivo = Vector3.zero;
    private bool puedeAtacar = true;
    private bool isJumping = false;

    private void Awake()
    {
        comportamientoBase = GetComponent<ComportamientoEnemigo>();
    }

    private void FixedUpdate()
    {
        if (!puedeAtacar)
            return;

        IObjetivoEnemigo objetivo = comportamientoBase.ObjetivoActual;
        if (objetivo == null)
            return;

        float distancia = Vector3.Distance(transform.position, comportamientoBase.ObjetivoActual.PosicionActual());
        if (distancia < distanciaSalto)
        {
            puedeAtacar = false;
            StartCoroutine(EjecutarSalto(objetivo));
        }
    }

    private IEnumerator EjecutarSalto(IObjetivoEnemigo objetivo)
    {
        isJumping = true;
        comportamientoBase.SuspenderComportamiento();

        Vector3 posicionInicial = transform.position;
        posicionObjetivo = objetivo.PosicionActual();

        //animacion de preparar el salto
        yield return new WaitForSeconds(1f);

        // Codigo corregido
        //Vector3 posicionInicial = transform.position;
        //Vector3 posicionObjetivo = objetivo.PosicionActual();

        float tiempo = 0f;

        //animacion de salto
        while (tiempo < duracionSalto)
        {
            tiempo += Time.deltaTime;

            float progreso = Mathf.Clamp01(tiempo / duracionSalto);

            Vector3 posicion = Vector3.Lerp(
                posicionInicial,
                posicionObjetivo,
                progreso
            );

            posicion.y += 4f * alturaSalto * progreso * (1f - progreso);

            transform.position = posicion;

            yield return null;
        }

        transform.position = posicionObjetivo;

        //animacion de aterrizaje
        yield return new WaitForSeconds(0.5f);

        comportamientoBase.ReanudarComportamiento();
        isJumping = false;
        posicionObjetivo = Vector3.zero;

        yield return new WaitForSeconds(descansoAtaque);
        puedeAtacar = true;
    }

    private void OnDrawGizmos()
    {
        if (comportamientoBase == null)
            comportamientoBase = GetComponent<ComportamientoEnemigo>();

        if (comportamientoBase == null)
            return;

        IObjetivoEnemigo objetivo = comportamientoBase.ObjetivoActual;

        // Sin objetivo: mostrar rango de salto
        if (objetivo == null)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, distanciaSalto);
            return;
        }

        float distancia = Vector3.Distance(transform.position, objetivo.PosicionActual());

        if ((distancia < distanciaSalto))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distanciaSalto);
        }

        if (!isJumping || posicionObjetivo == Vector3.zero)
            return;

        // Con objetivo: mostrar trayectoria
        Vector3 inicio = transform.position;
        Vector3 destino = posicionObjetivo;

        int segmentos = 20;
        Vector3 puntoAnterior = inicio;

        for (int i = 1; i <= segmentos; i++)
        {
            float progreso = (float)i / segmentos;

            Vector3 punto = Vector3.Lerp(inicio, destino, progreso);

            punto.y += 4f * alturaSalto * progreso * (1f - progreso);

            Gizmos.DrawLine(puntoAnterior, punto);

            puntoAnterior = punto;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(destino, 0.1f);
    }
}
