using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InventarioTemporal : MonoBehaviour
{
    [SerializeField] private Item itemPrefab;
    [SerializeField] private List<ElementoInventario> elementosIniciales = new List<ElementoInventario>();
    [SerializeField] private int limiteCantidad;
    private Inventario inventario;

    private void Start()
    {
        inventario = new Inventario();

        int chances = UnityEngine.Random.Range(0, 100);
        foreach (ElementoInventario elemento in elementosIniciales)
        {
            if (chances < elemento.Probabilidad)
                inventario.AgregarItem(elemento.ItemDatos, elemento.Cantidad);
        }

        Destroy(gameObject, 2f);
    }

    public bool IntentarAgregarElemento(ItemScriptableObject datos, int numCantidad, int numProbabilidad)
    {
        if(inventario.CantidadActual + numCantidad > limiteCantidad)
            return false;

        inventario.AgregarItem(datos, numCantidad);
        return true;
    }
    public bool RemoverElemento(ItemScriptableObject datos)
    {
        inventario.RemoverItem(datos);
        return true;
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return; 
        
        Collider col = GetComponent<Collider>();
        Vector3 topPosition = col.bounds.center + new Vector3(0, col.bounds.extents.y, 0);

        foreach (ItemScriptableObject item in inventario.AplanarInventario())
        {
            Item itemObjeto = Instantiate(itemPrefab, topPosition, Quaternion.identity);
            itemObjeto.Inicializar(item);
            itemObjeto.DroppearItem();
        }
    }
}
