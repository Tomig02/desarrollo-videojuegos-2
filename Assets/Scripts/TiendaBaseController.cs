
using UnityEngine;

public class TiendaBaseController : MonoBehaviour
{
    [SerializeField] private EconomiaBase economia;
    [SerializeField] private int precioMejora = 50;

    private int mejorasCompradas = 0;

    public void ComprarMejora()
    {
        if (economia == null)
        {
            Debug.LogError("No se asignó el sistema de economía.");
            return;
        }

        if (economia.GastarMonedas(precioMejora))
        {
            mejorasCompradas++;
            Debug.Log("Mejora comprada. Total: " + mejorasCompradas);
        }
        else
        {
            Debug.Log("No tenés monedas suficientes.");
        }
    }
}
