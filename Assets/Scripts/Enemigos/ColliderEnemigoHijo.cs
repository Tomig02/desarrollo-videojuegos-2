using UnityEngine;


public enum TriggerEnemigo 
{ 
    Vision,
    Danio
}

public class ColliderEnemigoHijo : MonoBehaviour
{
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private TriggerEnemigo triggerTipo;
    private ComportamientoEnemigo enemigo;


    private void Awake()
    {
        enemigo = GetComponentInParent<ComportamientoEnemigo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IObjetivoEnemigo objetivo))
        {
            enemigo.ColisionEntrada(objetivo, triggerTipo);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IObjetivoEnemigo objetivo))
        {
            enemigo.ColisionSalida(objetivo, triggerTipo);
        }
    }
}