
using UnityEngine;

public class EconomiaBase : MonoBehaviour
{
    public static EconomiaBase Instancia { get; private set; }

    [SerializeField] private int monedas = 0;

    public int Monedas => monedas;

    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AgregarMonedas(int cantidad)
    {
        if (cantidad <= 0) return;

        monedas += cantidad;
        Debug.Log("Monedas actuales: " + monedas);
    }

    public bool GastarMonedas(int cantidad)
    {
        if (cantidad <= 0 || monedas < cantidad)
            return false;

        monedas -= cantidad;
        Debug.Log("Monedas restantes: " + monedas);
        return true;
    }
}
