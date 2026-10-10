
using UnityEngine;

public class TiendaBaseController : MonoBehaviour
{
    [SerializeField] private int precioMejora = 50;

    private int mejorasCompradas = 0;

    public void ComprarMejora()
    {
        EconomiaBase economia = EconomiaBase.Instancia;

        if (economia == null)
        {
            Debug.LogError("No se encontró el sistema de economía.");
            return;
        }

        if (economia.GastarMonedas(precioMejora))
        {
            mejorasCompradas++;
            PlayerController.mejorasDeVida++;

            Debug.Log("Mejora comprada. Total: " + mejorasCompradas);
            Debug.Log("Nueva vida máxima: " + (100 + PlayerController.mejorasDeVida * 25));
        }
        else
        {
            Debug.Log("No tenés monedas suficientes.");
        }
    }


    public void VenderObjetos()
    {
        EconomiaBase economia = EconomiaBase.Instancia;

        if (economia == null)
        {
            Debug.LogError("No se encontró el sistema de economía.");
            return;
        }

        var objetos = PlayerController.inventarioJugador.ObtenerLista();

        int totalVenta = 0;

        foreach (var objeto in objetos)
        {
            totalVenta += objeto.Key.Valor * objeto.Value;
        }

        foreach (var objeto in objetos)
        {
            for (int i = 0; i < objeto.Value; i++)
            {
                PlayerController.inventarioJugador.RemoverItem(objeto.Key);
            }
        }

        economia.AgregarMonedas(totalVenta);

        Debug.Log("Objetos vendidos. Ganaste " + totalVenta + " monedas.");
    }

}
