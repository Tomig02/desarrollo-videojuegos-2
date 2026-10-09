using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ComportamientoEnemigo))]
public class EnemigoConSalto : MonoBehaviour
{
    private ComportamientoEnemigo comportamientoBase;
    [SerializeField] private float distanciaSalto = 2f;
    [SerializeField] private float duracionSalto = 0.5f;
    [SerializeField] private float alturaSalto = 1f;
    [SerializeField] private float descansoAtaque = 1f;

    private bool puedeAtacar = true;

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
        comportamientoBase.SuspenderComportamiento();

        //animacion de preparar el salto
        yield return new WaitForSeconds(1f);

        Vector3 posicionInicial = transform.position;
        Vector3 posicionObjetivo = objetivo.PosicionActual();

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

        yield return new WaitForSeconds(descansoAtaque);
        puedeAtacar = true;
    }
}
