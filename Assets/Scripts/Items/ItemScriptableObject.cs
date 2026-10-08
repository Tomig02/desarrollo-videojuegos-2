using UnityEngine;

[CreateAssetMenu(fileName = "NuevoItem", menuName = "Inventario/Item")]
public class ItemScriptableObject : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Nombre { get; private set; }
    [field: SerializeField] public int Valor { get; private set; }
}