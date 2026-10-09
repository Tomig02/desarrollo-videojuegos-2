using UnityEngine;

public class Jugador : MonoBehaviour, IObjetivoEnemigo
{
    public Vector3 PosicionActual()
    {
        return transform.position;
    }

    public void RecibirDanio(int cantidad)
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
