
using UnityEngine;

public class EconomiaBase : MonoBehaviour
{
    [SerializeField] private int monedas = 0;

    public int Monedas => monedas;

    public void AgregarMonedas(int cantidad)
    {
        if (cantidad <= 0) return;

        monedas += cantidad;
        Debug.Log("Monedas actuales: " + monedas);
    }

    public bool GastarMonedas(int cantidad)
    {
        if (cantidad <= 0 || monedas < cantidad)
        {
            return false;
        }

        monedas -= cantidad;
        Debug.Log("Monedas restantes: " + monedas);
        return true;
    }
}
