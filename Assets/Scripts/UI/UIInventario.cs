using UnityEngine;

public class UIInventario : MonoBehaviour
{
    [SerializeField] private Inventario testInventario;
    [SerializeField] private ItemScriptableObject itemPrefab;
    [SerializeField] private UIInventarioElemento elementoPrefab;
    [SerializeField] private Transform contenido;

    private void Start()
    {
        testInventario = new Inventario();
        if (itemPrefab != null)
        {
            testInventario.AgregarItem(itemPrefab, 3);
        }
        MostartInventario(testInventario);
    }

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
