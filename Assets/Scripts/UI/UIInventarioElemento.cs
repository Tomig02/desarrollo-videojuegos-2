using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventarioElemento : MonoBehaviour
{
    [SerializeField] private Image icono;
    [SerializeField] private TextMeshProUGUI nombre;
    [SerializeField] private TextMeshProUGUI cantidad;

    public void Inicializar(ItemScriptableObject item, int cantidad)
    {
        icono.sprite = item.Sprite;
        nombre.text = item.Nombre;
        this.cantidad.text = cantidad.ToString();
    }
}