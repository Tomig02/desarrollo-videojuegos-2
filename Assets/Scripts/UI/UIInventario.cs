using UnityEngine;

public class UIInventario : MonoBehaviour
{
    [SerializeField] private UIInventarioElemento elementoPrefab;
    [SerializeField] private Transform contenido;

    public void MostartInventario(Inventario inventario)
    {
        foreach (var llaveValor in inventario.ObtenerLista())
        {
            UIInventarioElemento elemento = Instantiate(elementoPrefab, transform);
            elemento.Inicializar(llaveValor.Key, llaveValor.Value);
            elemento.transform.SetParent(contenido, false);
        }
    }
}
