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
        if (datos == null || numCantidad <= 0) 
        {
            Debug.LogWarning("Intento de agregar un item nulo o con cantidad no positiva al inventario.");
            return;
        } 

        CantidadActual += numCantidad;

        if (items.TryGetValue(datos, out int cantidadExistente))
            items[datos] = cantidadExistente + numCantidad;
        else
            items[datos] = numCantidad;
    }
    public void RemoverItem(ItemScriptableObject datos)
    {
        if (datos == null) {
            Debug.LogWarning("Intento de remover un item nulo del inventario.");
            return; 
        }

        if (items.TryGetValue(datos, out int cantidadExistente))
        {
            CantidadActual--;
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
        List<ItemScriptableObject> listaAplanada = new List<ItemScriptableObject>(CantidadActual);

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
