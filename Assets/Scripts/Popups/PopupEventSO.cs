
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PopupEvent", menuName = "Events/Popup Event")]
public class PopupEventSO : ScriptableObject
{
    public event Action<string, string> OnPopupRequested;

    public void Raise(string titulo, string mensaje)
    {
        OnPopupRequested?.Invoke(titulo, mensaje);
    }
}
