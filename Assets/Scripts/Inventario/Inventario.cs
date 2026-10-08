using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventario
{
    private Dictionary<ItemScriptableObject, int> items = new();
    public int CantidadActual { get; private set; } = 0;

    public void AgregarItem(ItemScriptableObject datos, int numCantidad)
    {
        CantidadActual += numCantidad;

        if (items.TryGetValue(datos, out int cantidadExistente))
            items[datos] = cantidadExistente + numCantidad;
        else
            items[datos] = numCantidad;
    }
    public void RemoverItem(ItemScriptableObject datos)
    {
        var elemento = items.GetValueOrDefault(datos);

        CantidadActual--;
        if (elemento != null)
        {
            items[datos] -= 1;
            if (items[datos] <= 0)
                items.Remove(datos);
        }
    }

    public List<KeyValuePair<ItemScriptableObject, int>> ObtenerLista()
    {
        return items.ToList();
    }
    public List<ItemScriptableObject> AplanarInventario()
    {
        List<ItemScriptableObject> listaAplanada = new List<ItemScriptableObject>();
        foreach (var item in items)
        {
            for(int i = 0; i < item.Value; i++)
            {
                listaAplanada.Add(item.Key);
            }
        }
        return listaAplanada;
    }
}
