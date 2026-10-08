using System;
using UnityEngine;

[Serializable] public class ElementoInventario
{
    [field: SerializeField] public ItemScriptableObject ItemDatos { get; private set; }
    [field: SerializeField] public int Cantidad { get; private set; }
    [field: SerializeField] public int Probabilidad { get; private set; }

    public void AgregarCantidad(int cantidad)
    {
        Cantidad += cantidad;
    }
    public void ReducirCantidad(int cantidad)
    {
        Cantidad -= cantidad;
    }
    public ElementoInventario(ItemScriptableObject itemDatos, int cantidad, int probabilidad)
    {
        ItemDatos = itemDatos;
        Cantidad = cantidad;
        Probabilidad = probabilidad;
    }
}